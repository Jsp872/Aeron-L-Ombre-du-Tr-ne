using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Boss boss;

    private QuestManager qM;
    private EnemyManager eM;
    private EnemyStat eT;

    [HideInInspector] public SpriteRenderer spriteRenderer;
    [HideInInspector] public SpriteRenderer[] otherSpriteRenderer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boss = GetComponent<Boss>();
        qM = GetComponentInParent<QuestManager>();
        eM = GetComponentInParent<EnemyManager>();
        eT = GetComponent<EnemyStat>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (eT.differentSprite)
           otherSpriteRenderer = GetComponentsInChildren<SpriteRenderer>();
    }
    public IEnumerator TakeDamage(float damage)
    {
        eT.life -= damage;

        if (eT.haveAnim)
        {
            Animator enemyAnimator = GetComponent<Animator>();
            enemyAnimator.SetTrigger("Hurt");
        }
        if (!eT.differentSprite)
        {
            float color = (eT.maxLife - eT.life) / eT.maxLife;
            spriteRenderer.color = new Color(1f, 1f - color, 1f - color, 1f);
        }
        else
        {
            foreach (SpriteRenderer sr in otherSpriteRenderer)
            {
                float color = (eT.maxLife - eT.life) / eT.maxLife;
                sr.color = new Color(1f, 1f - color, 1f - color, 1f);
            }
        }


        if (eT.life <= 0)
        {
            if (eT.haveAnim)
            {
                Animator enemyAnimator = GetComponent<Animator>();
                enemyAnimator.SetTrigger("Die");
            }

            gameObject.layer = 8;
            if (!eT.bossCheck)
                qM.CheckEnemyForTheQuest(eT.valueOfEnemy);
            gameObject.GetComponentInChildren<CircleCollider2D>().enabled = false;

            if (boss != null)
            {
                boss.Die();
            }
            else if (!eT.bossCheck)
            {
                eM.enemies.Add(gameObject);
                qM.messageOfNumberEnemyKill.text = $"{qM.numberOfEnemyKill} / {qM.GetTheValueOfTheNumberOfEnemyToKill}";
            }

            yield return new WaitForSeconds(1f);

            if (eT.bossCheck)
                Destroy(gameObject);
            else
                gameObject.SetActive(false);
        }
    }
}