using System.Collections;
using UnityEngine;

public class FireBoss : Boss
{
    [SerializeField] GameObject showFirePower;

    [Header("Attack 1")]
    [SerializeField] GameObject fireBall;
    [SerializeField] Vector3 fireBallSpawn;

    [Header("Attack 2")]
    [SerializeField] GameObject lava;
    [SerializeField] int xMin;
    [SerializeField] int xMax;
    [SerializeField] int yMin;
    [SerializeField] int yMax;

    [Header("Attack 3")]
    [SerializeField] SpriteRenderer showPaternAttack3;
    [SerializeField] BoxCollider2D attack3Trigger;

    private CircleCollider2D circleCollider2D;


    private void Awake()
    {
        circleCollider2D = GetComponentInChildren<CircleCollider2D>();
        circleCollider2D.enabled = true;
    }

    public override void ResetAttack()
    {
        circleCollider2D.enabled = true;
        attack3Trigger.size = new Vector2(0.8760376f, 1.032974f);
        showPaternAttack3.gameObject.SetActive(false);
    }
    public override void Die()
    {
        base.Die();
        showFirePower.SetActive(true);
        playerStat.stockDamage += 1f;
        playerStat.damage += 1f;
    }
    public override IEnumerator Attack1()
    {
        circleCollider2D.enabled = false;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                Instantiate(fireBall, transform.position + fireBallSpawn, Quaternion.Euler(0, 0, j * 45));
                yield return new WaitForSeconds(0.1f);
            }
        }
        yield return new WaitForSeconds(1f);
        circleCollider2D.enabled = true;
        StartCoroutine(Attack());
    }

    public override IEnumerator Attack2()
    {
        circleCollider2D.enabled = false;
        for (int i = 0; i < 4; i++)
        {
            Instantiate(lava, new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax), 0), Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(1f);
        circleCollider2D.enabled = true;
        StartCoroutine(Attack());
    }

    public override IEnumerator Attack3()
    {
        showPaternAttack3.gameObject.SetActive(true);
        showPaternAttack3.color = Color.white;
        circleCollider2D.enabled = false;
        yield return new WaitForSeconds(2f);
        circleCollider2D.enabled = true;
        attack3Trigger.size = new Vector2(3, 3);
        showPaternAttack3.color = Color.red;
        yield return new WaitForSeconds(3f);
        attack3Trigger.size = new Vector2(0.8760376f, 1.032974f);
        showPaternAttack3.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        StartCoroutine(Attack());
    }
}