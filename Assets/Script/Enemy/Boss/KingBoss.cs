using System.Collections;
using UnityEngine;

public class KingBoss : Boss
{
    [SerializeField] BlockDungeon blockThunderDungeon;

    [Header("Attack 1")]
    [SerializeField] GameObject KnightPrefab;

    [Header("Attack 2")]
    [SerializeField] SpriteRenderer showPaternAttack2;
    [SerializeField] CircleCollider2D attack2Trigger;

    EnemyStat eS;
    private CircleCollider2D circleCollider2D;

    private BoxCollider2D bossCollider;
    [SerializeField] GameObject killEnemyGameobject;

    private void Awake()
    {
        bossCollider = GetComponent<BoxCollider2D>();
        eS = GetComponent<EnemyStat>();
        circleCollider2D = GetComponentInChildren<CircleCollider2D>();
    }

    public override void ResetAttack()
    {
        bossCollider.enabled = false;
        killEnemyGameobject.SetActive(true);
        circleCollider2D.enabled = true;
        eS.moveSpeed = 8;
        attack2Trigger.enabled = false;
        showPaternAttack2.gameObject.SetActive(false);
    }
    public override void killEnemy()
    {
        killEnemyGameobject.SetActive(false);
        bossCollider.enabled = true;
    }

    public override void Die()
    {
        base.Die();
        blockThunderDungeon.blockThunderDungeon = false;
        playerStat.maxStamina += 100;
        playerStat.stamina += 100;
        playerStat.OnStaminaChange(playerStat.maxStamina);
    }

    public override IEnumerator Attack1()
    {
        Instantiate(KnightPrefab, transform.position - new Vector3(-2, 0, 0), Quaternion.identity);
        yield return new WaitForSeconds(5f);
        StartCoroutine(Attack());
    }
    public override IEnumerator Attack2()
    {
        circleCollider2D.enabled = false;
        showPaternAttack2.gameObject.SetActive(true);
        showPaternAttack2.color = Color.white;
        yield return new WaitForSeconds(0.75f);
        circleCollider2D.enabled = true;
        attack2Trigger.enabled = true;
        showPaternAttack2.color = Color.red;
        yield return new WaitForSeconds(3f);
        attack2Trigger.enabled = false;
        showPaternAttack2.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        StartCoroutine(Attack());
    }

    public override IEnumerator Attack3()
    {
        eS.moveSpeed = 10;
        yield return new WaitForSeconds(2f);
        eS.moveSpeed = 7;
        yield return new WaitForSeconds(2f);
        StartCoroutine(Attack());
    }
}