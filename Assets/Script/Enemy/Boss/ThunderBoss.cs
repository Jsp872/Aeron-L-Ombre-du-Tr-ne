using System.Collections;
using UnityEngine;

public class ThunderBoss : Boss
{
    [SerializeField] GameObject firstCastleAnim;
    [SerializeField] GameObject secondCastleAnim;
    [SerializeField] GameObject showThunderPower;

    [Header("Attack 2")]
    [SerializeField] GetTriggerForAttackPatern attackTrigger;

    [Header("Attack 3")]
    [SerializeField] GameObject thunder;
    [SerializeField] int xMin;
    [SerializeField] int xMax;
    [SerializeField] int yMin;
    [SerializeField] int yMax;

    private EnemyStat eS;
    private CircleCollider2D circleCollider2D;

    private void Awake()
    {
        eS = GetComponent<EnemyStat>();
        circleCollider2D = GetComponentInChildren<CircleCollider2D>();
    }

    public override void ResetAttack()
    {
        circleCollider2D.enabled = true;
        eS.moveSpeed = 8;
        attackTrigger.ResetAttack();
        attackTrigger.gameObject.SetActive(false);
    }

    public override void Die()
    {
        base.Die();
        firstCastleAnim.SetActive(false);
        secondCastleAnim.SetActive(true);
        showThunderPower.SetActive(true);
        playerStat.stockMoveSpeed += 2;
        playerStat.moveSpeed += 2;
    }

    public override IEnumerator Attack1()
    {
        Debug.Log("Attack 1");
        eS.moveSpeed = 11;
        yield return new WaitForSeconds(2f);
        eS.moveSpeed = 7;
        yield return new WaitForSeconds(2f);
        StartCoroutine(Attack());
    }

    public override IEnumerator Attack2()
    {
        Debug.Log("Attack 2");
        circleCollider2D.enabled = false;
        attackTrigger.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        attackTrigger.ActiveAnimator();
        attackTrigger.SetActive(true);
        yield return new WaitForSeconds(2f);
        attackTrigger.ResetAttackThunder();
        circleCollider2D.enabled = true;
        yield return new WaitForSeconds(2f);
        StartCoroutine(Attack());
    }

    public override IEnumerator Attack3()
    {
        Debug.Log("Attack 3"); 
        circleCollider2D.enabled = false;
        for (int i = 0; i < 50; i++)
        {
            Instantiate(thunder, new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax), 0), Quaternion.identity);
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(1f);
        circleCollider2D.enabled = true;
        StartCoroutine(Attack());
    }
}