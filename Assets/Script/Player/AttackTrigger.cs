using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{




    [SerializeField] private float waitTimeForAttack;

    private bool waitForAttack;
    private SelectMagic sM;
    private PlayerStat pS;
    public List<Enemy> enemyInCollider = new List<Enemy>();

    private void Awake()
    {
        sM = GetComponentInParent<SelectMagic>();
        pS = GetComponentInParent<PlayerStat>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 7 && !waitForAttack)
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemyInCollider.Add(enemy);
            if (pS.attack)
            {
                foreach (Enemy e in new List<Enemy>(enemyInCollider))
                {
                    if (e != null)
                    {
                        GiveDamageToEnemy(e);
                    }
                }
            }

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 15)
        {
            if (pS.attack)
            {
                BlockDungeon blockDungeon = collision.gameObject.GetComponent<BlockDungeon>();
                DestroyBlockDungeon(collision.gameObject, blockDungeon);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 7)
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemyInCollider.Remove(enemy);
        }
    }

    private void GiveDamageToEnemy(Enemy enemy)
    {
        Vector2 forceDirection = pS.lastMoveDirection;
        if (forceDirection == Vector2.zero) forceDirection = Vector2.right;

        enemy.rb.AddForce(forceDirection * pS.propulsionForce, ForceMode2D.Impulse);
        StartCoroutine(enemy.TakeDamage(pS.damage));
        EnemyStat enemyStat = enemy.gameObject.GetComponent<EnemyStat>();
        if (enemyStat.life <= 0)
        {
            enemyInCollider.Remove(enemy);
        }
        StartCoroutine(WaitToAttack());
    }

    IEnumerator WaitToAttack()
    {
        waitForAttack = true;
        yield return new WaitForSeconds(waitTimeForAttack);
        waitForAttack = false;
    }

    private void DestroyBlockDungeon(GameObject blockDungeonObject, BlockDungeon blockDungeon)
    {
        if (blockDungeon.blockDungeonNumber == sM.magicNumber && pS.activateMagic && !blockDungeon.blockThunderDungeon)
        {
            blockDungeonObject.gameObject.SetActive(false);
        }
    }
}