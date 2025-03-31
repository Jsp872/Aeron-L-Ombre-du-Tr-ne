using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool finalBoss;
    public bool falseFinalBoss;
    public int moveSpeed = 2;
    public int damage = 1;
    public int propulsionForce = 5;
    [SerializeField] bool boss;
    [SerializeField] int life = 3;
    Animator animator;
    public Rigidbody2D rb;

    [SerializeField] GameObject firstCastleAnim;
    [SerializeField] GameObject secondCastleAnim;
    [SerializeField] GameObject blockFireDungeon;
    [SerializeField] GameObject blockIceDungeon;
    [SerializeField] GameObject blockThunderDungeon;

    [SerializeField] GameObject showWindPower;
    [SerializeField] GameObject showFirePower;
    [SerializeField] GameObject showIcePower;
    [SerializeField] GameObject showThunderPower;

    [SerializeField] TileMapManager tileMapManager;
    [SerializeField] TileMapManager tileMapManagerForEnteringCastle;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    public void TakeDamage(int damage)
    {
        life -= damage;
        if (finalBoss || falseFinalBoss)
        {
            animator.SetTrigger("Hurt");
        }
        if (life <= 0)
        {
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        bool checkIfFalseFinalBoss = false;
        gameObject.layer = 8;
        gameObject.GetComponentInChildren<CircleCollider2D>().enabled = false;
        if (finalBoss || falseFinalBoss)
        {
            checkIfFalseFinalBoss = true;
            animator.SetTrigger("Die");
            tileMapManager.OpenBossDoor();
        }
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
        if (boss)
        {
            tileMapManager.OpenBossDoor();
            tileMapManagerForEnteringCastle.numberOfBossKilled++;
            if (tileMapManagerForEnteringCastle.numberOfBossKilled == 1)
            {
                blockFireDungeon.SetActive(false);
                showWindPower.SetActive(true);
            }
            else if (tileMapManagerForEnteringCastle.numberOfBossKilled == 2)
            {
                blockIceDungeon.SetActive(false);
                showFirePower.SetActive(true);
            }
            else if (tileMapManagerForEnteringCastle.numberOfBossKilled == 3)
            {
                tileMapManagerForEnteringCastle.OpenBossDoor();
                showIcePower.SetActive(true);
            }
            else if (tileMapManagerForEnteringCastle.numberOfBossKilled == 4)
            {
                firstCastleAnim.SetActive(false);
                secondCastleAnim.SetActive(true);
                showThunderPower.SetActive(true);
            }
        }
        if (checkIfFalseFinalBoss)
        {
            blockThunderDungeon.SetActive(false);
        }
    }

}