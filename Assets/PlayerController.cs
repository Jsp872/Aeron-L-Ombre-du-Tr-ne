using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static event Action<int> OnLifeChanged;
    public static event Action<int> OnStaminaChanged;
    public static event Action<int> OnManaChanged;


    [SerializeField] GameObject player;
    [SerializeField] GameObject UiPause;
    private Vector3 direction;
    public float moveSpeed = 5f;
    [SerializeField] private InputActionReference inputActionMove;
    private Animator animator;
    [SerializeField] private BoxCollider boxCollider;

    [SerializeField] int life= 100;
    int stamina = 100;
    int mana = 100;
    float stockMoveSpeed = 5;

    [SerializeField] GameObject UIDefeat;
    public Rigidbody rb;

    bool attack;
    bool isRunning;
    bool block;
    public bool isPause;

    private Coroutine sprintCoroutine;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        StartCoroutine(RecoveryStaminaAndMana());
    }

    private void Update()
    {
        direction = inputActionMove.action.ReadValue<Vector3>().normalized;
        Vector3 move = new Vector3(direction.x, 0, direction.z) * moveSpeed * Time.deltaTime;
        transform.position += move;

        animator.SetBool("IsMoving", direction.magnitude > 0);

        DirectionOfMove();

        TestMana();
    }

    public void TestMana()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (mana >= 50)
            {
                mana -= 50;
                OnManaChanged?.Invoke(mana);
            }
        }
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
            if (mana < 100)
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
                transform.rotation = Quaternion.Euler(0, 90, 0);
            else if (direction.x < 0)
                transform.rotation = Quaternion.Euler(0, -90, 0);
            else if (direction.z > 0 && direction.x == 0)
                transform.rotation = Quaternion.Euler(0, 0, 0);
            else if (direction.z < 0 && direction.x == 0)
                transform.rotation = Quaternion.Euler(0, 180, 0);
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
            rb.AddForce(transform.forward * 100, ForceMode.Impulse);
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
            moveSpeed *= 2;
            sprintCoroutine = StartCoroutine(DrainStamina());
        }
        animator.SetBool("Running", true);
    }

    public void OnUnSprint()
    {
        isRunning = false;
        moveSpeed = stockMoveSpeed;

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
            player.SetActive(false);
            yield return new WaitForSeconds(0.1f);
            player.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            count++;
        }
    }
}
