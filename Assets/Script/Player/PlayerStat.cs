using System;
using System.Collections;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    [Header("Player Stats")]
    public float maxLife = 100;
    public float maxStamina = 100;
    public float maxMana = 100;
    public float stockMoveSpeed = 5;
    [HideInInspector] public float life = 100;
    [HideInInspector] public float stamina = 100;
    [HideInInspector] public float mana = 100;
    [HideInInspector] public float moveSpeed = 5f;

    [Header("Attack Stats")]
    public int propulsionForce;
    public float stockDamage;
    [HideInInspector] public bool attack;
    [HideInInspector] public float damage;

    [Header("Magic Stats")]
    [HideInInspector] public bool fire;
    [HideInInspector] public bool wind;
    [HideInInspector] public bool ice;
    [HideInInspector] public bool thunder;
    [HideInInspector] public bool activateMagic;

    [Header("Action Stats")]
    public bool isPause;
    public bool isRunning;
    public bool isInvincible;
    [HideInInspector] public bool waitForAttack;
    [HideInInspector] public bool isBlocking;
    [HideInInspector] public Vector2 lastMoveDirection;

    [Header("Reference")]
    public BoxCollider2D attackTrigger;
    public ParticleSystem particleForIce;
    public GameObject doorHouse;
    public GameObject UIDefeat;
    public GameObject UiPause;
    [HideInInspector] public SpriteRenderer spriteRenderer;
    [HideInInspector] public Animator animator;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public BoxCollider2D boxCollider;



    public static event Action<float> OnLifeChanged;
    public static event Action<float> OnStaminaChanged;
    public static event Action<float> OnManaChanged;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(RecoveryStaminaAndMana());
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void OnLifeChange(float value)
    {
        OnLifeChanged?.Invoke(value);
    }

    public void OnManaChange(float value)
    {
        OnManaChanged?.Invoke(value);
    }

    public void OnStaminaChange(float value)
    {
        OnStaminaChanged?.Invoke(value);
    }

    IEnumerator RecoveryStaminaAndMana()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            if (stamina < maxStamina && !attack && !isRunning && !isBlocking)
            {
                stamina = Mathf.Min(stamina + 20, maxStamina);
                OnStaminaChanged?.Invoke(stamina);
            }
            if (mana < maxMana && !activateMagic)
            {
                mana = Mathf.Min(mana + 10, maxMana);
                OnManaChanged?.Invoke(mana);
            }
            if (life < maxLife && !attack && !isRunning && !isBlocking && !waitForAttack)
            {
                life = Mathf.Min(life + 1, maxLife);
                OnLifeChanged?.Invoke(life);
            }
        }
    }
}