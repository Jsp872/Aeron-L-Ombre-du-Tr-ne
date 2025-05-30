using System.Collections;
using UnityEngine;

public class WindBoss : Boss
{
    [SerializeField] GameObject showWindPower;
    private BoxCollider2D bossCollider;
    [SerializeField] GameObject killEnemyGameobject;

    [Header("Attack 1")]
    [SerializeField] GameObject fairyPrefab;

    [Header("Attack 2")]
    [SerializeField] SpriteRenderer showPaternAttack2;
    [SerializeField] BoxCollider2D attack2Trigger;

    [Header("Attack 3")]
    [SerializeField] SpriteRenderer showFirstPaternAttack3;
    [SerializeField] SpriteRenderer showSecondPaternAttack3;
    [SerializeField] BoxCollider2D attack3FirstTrigger;
    [SerializeField] BoxCollider2D attack3secondTrigger;

    private CircleCollider2D circleCollider2D;

    private void Awake()
    {
        circleCollider2D = GetComponentInChildren<CircleCollider2D>();
        bossCollider = GetComponent<BoxCollider2D>();
    }
    public override void Die()
    {
        base.Die();
        showWindPower.SetActive(true);
        playerStat.maxMana += 100;
        playerStat.mana += 100;
        playerStat.OnManaChange(playerStat.maxMana);
    }

    public override void ResetAttack()
    {
        bossCollider.enabled = false;
        killEnemyGameobject.SetActive(true);
        circleCollider2D.enabled = true;
        attack2Trigger.size = new Vector2(2, 2);
        attack3FirstTrigger.size = new Vector2(2, 2);
        attack3secondTrigger.size = new Vector2(2, 2);
        showPaternAttack2.gameObject.SetActive(false);
        showFirstPaternAttack3.gameObject.SetActive(false);
        showSecondPaternAttack3.gameObject.SetActive(false);
    }

    public override void killEnemy()
    {
        killEnemyGameobject.SetActive(false);
        bossCollider.enabled = true;
    }

    public override IEnumerator Attack1()
    {
        Instantiate(fairyPrefab, transform.position - new Vector3(-2,0,0), Quaternion.identity);
        Instantiate(fairyPrefab, transform.position - new Vector3(2,0,0), Quaternion.identity);
        yield return new WaitForSeconds(5f);
        StartCoroutine(Attack());
    }

    public override IEnumerator Attack2()
    {
        circleCollider2D.enabled = false;
        showPaternAttack2.gameObject.SetActive(true);
        showPaternAttack2.color = Color.white;
        yield return new WaitForSeconds(2f);

        attack2Trigger.size = new Vector2(7, 7);
        showPaternAttack2.color = Color.red;
        yield return new WaitForSeconds(3f);

        circleCollider2D.enabled = true;
        attack2Trigger.size = new Vector2(2, 2);
        showPaternAttack2.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);

        StartCoroutine(Attack());
    }

    public override IEnumerator Attack3()
    {
        circleCollider2D.enabled = false;
        showFirstPaternAttack3.gameObject.SetActive(true);
        showFirstPaternAttack3.color = Color.white;
        showSecondPaternAttack3.gameObject.SetActive(true);
        showSecondPaternAttack3.color = Color.white;
        yield return new WaitForSeconds(2f);

        attack3FirstTrigger.size = new Vector2(20, 2);
        attack3secondTrigger.size = new Vector2(2, 40);
        showFirstPaternAttack3.color = Color.red;
        showSecondPaternAttack3.color = Color.red;
        yield return new WaitForSeconds(3);

        circleCollider2D.enabled = true;
        attack3FirstTrigger.size = new Vector2(2, 2);
        attack3secondTrigger.size = new Vector2(2, 2);
        showFirstPaternAttack3.gameObject.SetActive(false);
        showSecondPaternAttack3.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);

        StartCoroutine(Attack());
    }
}