using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public bool finalBoss;
    public bool falseFinalBoss;

    public PlayerStat playerStat;

    private TileMapManager tileMapManager;
    private BossKilledManager bKM;
    private EnemyStat eS;
    private int attack;
    private bool firstAttack;

    private void OnEnable()
    {
        killEnemy();
        tileMapManager = transform.parent.GetComponentInChildren<TileMapManager>();
        bKM = GetComponentInParent<BossKilledManager>();
        eS = GetComponent<EnemyStat>();
        eS.life = eS.maxLife;
        transform.position = eS.initialPosition;
        firstAttack = true;
        StartCoroutine(Attack());
    }

    private void OnDisable()
    {
        ResetAttack();
    }

    public virtual void ResetAttack()
    {
    }

    public virtual void killEnemy()
    {

    }

    public virtual void Die()
    {
        tileMapManager.OpenBossDoor();
        if (!finalBoss && !falseFinalBoss)
        {
            bKM.numberOfBossKilled++;
        }
    }

    public virtual IEnumerator Attack()
    {
        if (firstAttack)
        {
            yield return new WaitForSeconds(2f);
            firstAttack = false;
        }
        attack = Random.Range(0, 3);
        switch (attack)
        {
            case 0:
                StartCoroutine(Attack1());
                break;
            case 1:
                StartCoroutine(Attack2());
                break;
            case 2:
                StartCoroutine(Attack3());
                break;
            default:
                break;
        }
    }

    public virtual IEnumerator Attack1()
    {
        yield return null;
    }

    public virtual IEnumerator Attack2()
    {
        yield return null;
    }

    public virtual IEnumerator Attack3()
    {
        yield return null;
    }
}