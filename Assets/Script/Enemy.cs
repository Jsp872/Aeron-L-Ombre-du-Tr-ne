using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int moveSpeed = 2;
    public int damage = 1;
    public int propulsionForce = 5;
    public bool bossCheck;
    public bool inverseLook;
    public int valueOfEnemy;
    public Vector2 initialPosition;
    public float life;
    public int maxLife;

    public Rigidbody2D rb;

    Boss boss;

    QuestManager qM;

    EnemyManager eM;

    private void Awake()
    {
        initialPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        boss = GetComponent<Boss>();
        qM = GetComponentInParent<QuestManager>();
        eM = GetComponentInParent<EnemyManager>();
        life = maxLife;
    }
    public IEnumerator TakeDamage(float damage)
    {
        life -= damage;
        if (bossCheck)
        {
            if (boss.falseFinalBoss || boss.finalBoss)
            {
                Animator bossAnimator = GetComponent<Animator>();
                bossAnimator.SetTrigger("Hurt");
            }
        }

        if (life <= 0)
        {
            gameObject.layer = 8;
            qM.CheckEnemyForTheQuest(valueOfEnemy);
            gameObject.GetComponentInChildren<CircleCollider2D>().enabled = false;
            if (boss != null)
                boss.Die();
            else 
                eM.enemies.Add(gameObject);
            yield return new WaitForSeconds(1f);

            gameObject.SetActive(false);

        }
    }

    private void OnDisable()
    {
        if (!bossCheck)
        {
            transform.position = initialPosition;
        }
    }
}