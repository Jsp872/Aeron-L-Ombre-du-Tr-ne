using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static event Action<int> OnLifeChanged;
    public static event Action<int> OnStaminaChanged;
    public static event Action<int> OnManaChanged;

    public Vector2 lastMoveDirection { get; private set; }

    public bool ice;

    SpriteRenderer spriteRenderer;
    [SerializeField] GameObject UiPause;
    private Vector3 direction;
    public float moveSpeed = 5f;
    [SerializeField] InputActionReference inputActionMove;
    private Animator animator;
    [SerializeField] private BoxCollider2D boxCollider;

    [SerializeField] int life= 100;
    int stamina = 100;
    public int mana = 100;
    float stockMoveSpeed = 5;

    [SerializeField] GameObject UIDefeat;
    public Rigidbody2D rb;

    bool attack;
    public bool isRunning;
    bool block;
    public bool isPause;

    private Coroutine sprintCoroutine;

    [SerializeField] float propulsionForce;

    PlayerMagic playerMagic;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        StartCoroutine(RecoveryStaminaAndMana());
        playerMagic = GetComponent<PlayerMagic>();
    }

    private void FixedUpdate()
    {
        direction = inputActionMove.action.ReadValue<Vector2>().normalized;
        if (direction != Vector3.zero)
            lastMoveDirection = direction;

        Vector3 move = new Vector2(direction.x, direction.y) * moveSpeed * Time.deltaTime;
        transform.position += move;

        animator.SetBool("IsMoving", direction.magnitude > 0);
        DirectionOfMove();
        if (mana <= 0) 
            playerMagic.OnActivateMagic();
    }


    IEnumerator RecoveryStaminaAndMana()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            if (stamina < 100 && !attack && !isRunning && !block)
            {
                stamina = Mathf.Min(stamina + 20, 100);
                OnStaminaChanged?.Invoke(stamina);
            }
            if (mana < 100 && !playerMagic.activate)
            {
                mana = Mathf.Min(mana + 10, 100);
                OnManaChanged?.Invoke(mana);
            }
        }
    }

    private void DirectionOfMove()
    {
        if (!isPause)
        {
            if (direction.x > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                boxCollider.offset = new Vector2(2.060688f, -1.463209f);
                boxCollider.size = new Vector2(2.59382343f, 4.44918299f);
            }
            else if (direction.x < 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                boxCollider.offset = new Vector2(2.060688f, -1.463209f);
                boxCollider.size = new Vector2(2.59382343f, 4.44918299f);
            }
            else if (direction.y > 0)
            {
                boxCollider.offset = new Vector2(0.032356739f, 1.15704775f);
                boxCollider.size = new Vector2(3.27256441f, 2.77600956f);
            }
            else if (direction.y < 0)
            {
                boxCollider.offset = new Vector2(0.032356739f, -4.789396f);
                boxCollider.size = new Vector2(3.27256441f, 2.297656f);
            }
        }
    }

    public void OnAttack()
    {
        if (stamina >= 20 && !attack && !isPause)
        {
            stamina -= 20;
            OnStaminaChanged?.Invoke(stamina);
            animator.SetTrigger("Attack");
            boxCollider.enabled = true;
            StartCoroutine(TimeOfAttack());
        }
    }

    public void OnDash()
    {
        if (stamina >= 50 && !isPause)
        {
            stamina -= 50;
            OnStaminaChanged?.Invoke(stamina);

            Vector2 dashDirection = lastMoveDirection;
            if (dashDirection == Vector2.zero) dashDirection = Vector2.right;

            rb.AddForce(dashDirection * propulsionForce, ForceMode2D.Impulse);

            animator.SetTrigger("Dash");
            boxCollider.enabled = true;
            StartCoroutine(TimeOfAttack());
        }
    }


    public void OnSprint()
    {
        if (stamina > 0 && sprintCoroutine == null && !isPause)
        {
            isRunning = true;
            moveSpeed = 10;
            sprintCoroutine = StartCoroutine(DrainStamina());
        }
        animator.SetBool("Running", true);
    }

    public void OnUnSprint()
    {
        isRunning = false;
        moveSpeed = 5;

        if (sprintCoroutine != null)
        {
            StopCoroutine(sprintCoroutine);
            sprintCoroutine = null;
        }
        animator.SetBool("Running", false);
    }

    public void OnBlock()
    {
        if (!isPause)
        {
            moveSpeed = stockMoveSpeed / 2;

            block = true;
            animator.SetBool("Block", true);
        }
    }

    public void OnUnBlock()
    {
        moveSpeed = stockMoveSpeed;
        block = false;
        animator.SetBool("Block", false);
    }

    public void OnPause()
    {
        isPause = true;
        Time.timeScale = 0;
        UiPause.SetActive(true);
    }

    IEnumerator DrainStamina()
    {
        while (isRunning && stamina > 0)
        {
            stamina = Mathf.Max(stamina - 10, 0);
            OnStaminaChanged?.Invoke(stamina);

            if (stamina == 0)
                OnUnSprint();

            yield return new WaitForSeconds(1f);
        }
    }



    IEnumerator TimeOfAttack()
    {
        attack = true;
        yield return new WaitForSeconds(0.75f);
        boxCollider.enabled = false;
        attack = false;
    }


    public void TakeDamage(float damage)
    {
        if (ice)
            damage /= 2;

        if (block && stamina >= 30)
        {
            damage /= 2;
                stamina -= 30;
            OnStaminaChanged?.Invoke(stamina);
        }

        int finalDamage = Mathf.FloorToInt(damage);
        if (finalDamage > 0)
        {
            life -= finalDamage;
            animator.SetTrigger("Hit");
            StartCoroutine(HitAnim());
            OnLifeChanged?.Invoke(life);
        }


        if (life <= 0)
        {
            Destroy(gameObject);
            Time.timeScale = 0;
            UIDefeat.SetActive(true);
        }
    }


    IEnumerator HitAnim()
    {
        int count = 0;
        int maxCount = 3;
        while (count < maxCount)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(0.1f);
            count++;
        }
    }
}
