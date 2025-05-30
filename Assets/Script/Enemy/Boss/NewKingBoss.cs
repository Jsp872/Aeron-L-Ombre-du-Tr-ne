using System.Collections;
using UnityEngine;

public class NewKingBoss : Boss
{
    [SerializeField] GameObject victoryScreen;

    [Header("Attack 1")]
    [SerializeField] GameObject fireBall;
    [SerializeField] Vector3 fireBallSpawn;
    [SerializeField] SpriteRenderer showFirstPaternAttack1;
    [SerializeField] SpriteRenderer showSecondPaternAttack1;
    [SerializeField] BoxCollider2D attack1FirstTrigger;
    [SerializeField] BoxCollider2D attack1secondTrigger;

    [Header("Attack 2")]
    [SerializeField] GameObject thunder;
    [SerializeField] GameObject lava;
    [SerializeField] int xMin;
    [SerializeField] int xMax;
    [SerializeField] int yMin;
    [SerializeField] int yMax;
    [SerializeField] SpriteRenderer showPaternAttack2;
    [SerializeField] PolygonCollider2D attack2Trigger;
    [SerializeField] Vector2 patern2side;
    [SerializeField] Vector2 patern2top;
    [SerializeField] Vector2 patern2bottom;

    [Header("Attack 3")]
    [SerializeField] GetTriggerForAttackPatern attackTriggerPart1;
    [SerializeField] GetTriggerForAttackPatern attackTrigger1;
    [SerializeField] GetTriggerForAttackPatern attackTrigger2;
    [SerializeField] GetTriggerForAttackPatern attackTrigger3;

    EnemyStat enemyStat;
    CircleCollider2D circleCollider2D;

    private void Awake()
    {
        enemyStat = GetComponent<EnemyStat>();
        circleCollider2D = GetComponentInChildren<CircleCollider2D>();
    }


    public override void Die()
    {
        base.Die();
        StartCoroutine(Victory());
    }
    public override void ResetAttack()
    {
        circleCollider2D.enabled = true;
        attack1FirstTrigger.size = new Vector2(2, 2);
        attack1secondTrigger.size = new Vector2(2, 2);
        attack2Trigger.enabled = false;
        attackTriggerPart1.ResetAttack();
        attackTrigger1.ResetAttack();
        attackTrigger2.ResetAttack();
        attackTrigger3.ResetAttack();
        showPaternAttack2.gameObject.SetActive(false);
        showFirstPaternAttack1.gameObject.SetActive(false);
        showSecondPaternAttack1.gameObject.SetActive(false);
    }

    public IEnumerator Victory()
    {
        yield return new WaitForSeconds(1f);
        victoryScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public override IEnumerator Attack1()
    {
        StartCoroutine(Attack1Part1());
        StartCoroutine(Attack1Part2());
        yield return new WaitForSeconds(4f);
        StartCoroutine(Attack());
    }

    private IEnumerator Attack1Part1()
    {
        showFirstPaternAttack1.gameObject.SetActive(true);
        showFirstPaternAttack1.color = Color.white;
        showSecondPaternAttack1.gameObject.SetActive(true);
        showSecondPaternAttack1.color = Color.white;
        yield return new WaitForSeconds(1.5f);

        attack1FirstTrigger.size = new Vector2(100, 2);
        attack1secondTrigger.size = new Vector2(2, 100);
        showFirstPaternAttack1.color = Color.red;
        showSecondPaternAttack1.color = Color.red;
        yield return new WaitForSeconds(1.7f);

        attack1FirstTrigger.size = new Vector2(2, 2);
        attack1secondTrigger.size = new Vector2(2, 2);
        showFirstPaternAttack1.gameObject.SetActive(false);
        showSecondPaternAttack1.gameObject.SetActive(false);
    }

    private IEnumerator Attack1Part2()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                Instantiate(fireBall, transform.position + fireBallSpawn, Quaternion.Euler(0, 0, j * 45));
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    public override IEnumerator Attack2()
    {
        StartCoroutine(Attack2Part1());
        StartCoroutine(Attack2Part2());
        StartCoroutine(Attack2Part3());
        yield return new WaitForSeconds(3.4f);
        StartCoroutine(Attack());
    }

    private IEnumerator Attack2Part1()
    {
        for (int i = 0; i < 16; i++)
        {
            Instantiate(lava, new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax), 0), Quaternion.identity);
            yield return new WaitForSeconds(0.15f);
        }
    }

    private IEnumerator Attack2Part2()
    {
        for (int i = 0; i < 100; i++)
        {
            Instantiate(thunder, new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax), 0), Quaternion.identity);
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(1.4f);
    }

    private IEnumerator Attack2Part3()
    {
        showPaternAttack2.gameObject.SetActive(true);
        showPaternAttack2.color = Color.white;
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
            showPaternAttack2.gameObject.transform.localPosition = patern2side;
            rotation = new Vector3(0, 0, 90);
        }
        else if (y > x && dir.y > 0)
        {
            showPaternAttack2.gameObject.transform.localPosition = patern2top;
            rotation = new Vector3(0, 0, 180);
        }
        else if (y > x && dir.y < 0)
        {
            showPaternAttack2.gameObject.transform.localPosition = patern2bottom;
            rotation = new Vector3(0, 0, 0);
        }
        showPaternAttack2.gameObject.transform.localRotation = Quaternion.Euler(rotation);


        showPaternAttack2.color = Color.white;
        yield return new WaitForSeconds(1f);
        attack2Trigger.enabled = true;
        showPaternAttack2.color = Color.cyan;
        yield return new WaitForSeconds(1.4f);
        attack2Trigger.enabled = false;
        showPaternAttack2.gameObject.SetActive(false);
    }

    public override IEnumerator Attack3()
    {
        StartCoroutine(Attack3Part1());
        StartCoroutine(Attack3Part2());
        circleCollider2D.enabled = false;
        yield return new WaitForSeconds(6f);
        circleCollider2D.enabled = true;
        yield return new WaitForSeconds(1f);

        StartCoroutine(Attack());
    }

    private IEnumerator Attack3Part1()
    {
        attackTriggerPart1.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        attackTriggerPart1.ActiveAnimator();
        attackTriggerPart1.SetActive(true);
        yield return new WaitForSeconds(2f);
        attackTriggerPart1.ResetAttackThunder();
        yield return new WaitForSeconds(2f);
    }

    private IEnumerator Attack3Part2()
    {
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
        attackTrigger1.ResetAttack();
        attackTrigger2.ResetAttack();
        attackTrigger3.ResetAttack();
        yield return new WaitForSeconds(2f);
    }
}