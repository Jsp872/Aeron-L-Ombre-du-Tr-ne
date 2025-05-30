using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBoss : Boss
{
    [SerializeField] GameObject showIcePower;
    [SerializeField] TileMapManager tileMapManagerForEnteringCastle;
    private CircleCollider2D circleCollider2D;
    private EnemyStat enemyStat;

    [Header("Attack 1")]
    [SerializeField] GetTriggerForAttackPatern attackTrigger1;
    [SerializeField] GetTriggerForAttackPatern attackTrigger2;
    [SerializeField] GetTriggerForAttackPatern attackTrigger3;

    [Header("Attack 2")]
    [SerializeField] GameObject iceSpike;
    public List<int> YValue1 = new List<int> { 66, 69, 72, 75, 78, 81, 84 };
    public List<float> YValue2 = new List<float> { 67.5f, 70.5f, 73.5f, 76.5f, 79.5f, 82.5f };

    [Header("Attack 3")]
    [SerializeField] SpriteRenderer showPaternAttack3;
    [SerializeField] PolygonCollider2D attack3Trigger;
    [SerializeField] Vector2 patern2side;
    [SerializeField] Vector2 patern2top;
    [SerializeField] Vector2 patern2bottom;

    private void Awake()
    {
        circleCollider2D = GetComponentInChildren<CircleCollider2D>();
        enemyStat = GetComponent<EnemyStat>();
        circleCollider2D.enabled = true;
    }

    public override void ResetAttack()
    {
        circleCollider2D.enabled = true;
        attackTrigger1.ResetAttack();
        attackTrigger2.ResetAttack();
        attackTrigger3.ResetAttack();
        attack3Trigger.enabled = false;
        showPaternAttack3.gameObject.SetActive(false);
    }
    public override void Die()
    {
        base.Die();
        tileMapManagerForEnteringCastle.OpenBossDoor();
        showIcePower.SetActive(true);
        playerStat.maxLife += 100;
        playerStat.life += 100;
        playerStat.OnLifeChange(playerStat.maxLife);
    }

    public override IEnumerator Attack1()
    {
        circleCollider2D.enabled = false;
        attackTrigger1.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        attackTrigger1.SetActive(true);
        attackTrigger1.ChangeColorInCyan();
        attackTrigger2.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        attackTrigger2.SetActive(true);
        attackTrigger2.ChangeColorInCyan();
        attackTrigger3.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        attackTrigger3.SetActive(true);
        attackTrigger3.ChangeColorInCyan();
        yield return new WaitForSeconds(1f);
        circleCollider2D.enabled = true;
        attackTrigger1.ResetAttack();
        attackTrigger2.ResetAttack();
        attackTrigger3.ResetAttack();
        yield return new WaitForSeconds(1f);
        StartCoroutine(Attack());
    }

    public override IEnumerator Attack2()
    {
        circleCollider2D.enabled = false;
        List<int> value = new List <int>(YValue1);
        List<float> value2 = new List<float>(YValue2);
        Quaternion rotation = Quaternion.Euler(0, 0, 90);
        Quaternion rotation2 = Quaternion.Euler(0, 0, -90);
        for (int i = 0; i < 4; i++)
        {
            int index = Random.Range(0, value.Count);
            int randomY = value[index];
            value.RemoveAt(index);
            Instantiate(iceSpike, new Vector3(73, randomY, 0),rotation);
        }
        for (int i = 0; i < 3; i++)
        {
            int index = Random.Range(0, value2.Count);
            float randomY = value2[index];
            value2.RemoveAt(index);
            Instantiate(iceSpike, new Vector3(79.2f, randomY, 0),rotation2);
        }
        yield return new WaitForSeconds(3f);
        circleCollider2D.enabled = true;
        yield return new WaitForSeconds(1f);
        StartCoroutine(Attack());
    }

    public override IEnumerator Attack3()
    {
        showPaternAttack3.gameObject.SetActive(true);
        circleCollider2D.enabled = false;
        Vector3 rotation = Vector3.zero;
        Vector2 dir = enemyStat.lastDirection;
        float x = dir.x;
        float y = dir.y;
        if (x < 0)
        {
            x = -x;
        }
        if (y < 0)
        {
            y = -y;
        }
        if (x >= y)
        {
            showPaternAttack3.gameObject.transform.localPosition = patern2side;
            rotation = new Vector3(0, 0, 90);
        }
        else if (y > x && dir.y > 0)
        {
            showPaternAttack3.gameObject.transform.localPosition = patern2top;
            rotation = new Vector3(0, 0, 180);
        }
        else if (y > x && dir.y < 0)
        {
            showPaternAttack3.gameObject.transform.localPosition = patern2bottom;
            rotation = new Vector3(0, 0, 0);
        }
        showPaternAttack3.gameObject.transform.localRotation = Quaternion.Euler(rotation);


        showPaternAttack3.color = Color.white;
        yield return new WaitForSeconds(2f);
        attack3Trigger.enabled = true;
        showPaternAttack3.color = Color.cyan;
        yield return new WaitForSeconds(1.5f);
        attack3Trigger.enabled = false;
        showPaternAttack3.gameObject.SetActive(false);
        circleCollider2D.enabled = true;
        yield return new WaitForSeconds(1f);
        StartCoroutine(Attack());
    }
}