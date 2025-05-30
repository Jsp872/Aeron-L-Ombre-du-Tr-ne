using System.Collections;
using UnityEngine;

public class EnemyDetectorTrigger : MonoBehaviour
{
    private Transform target;
    private bool oneTimeSaveScale;
    private float scaleSave;
    private PlayerStat pS;
    [SerializeField] int iceDamage;
    [SerializeField] float propulsionForce;
    [SerializeField] float waitTimeForAttack;

    private PlayerTakeDamage pTD;

    private void Awake()
    {
        target = GetComponent<Transform>();
        pS = GetComponentInParent<PlayerStat>();
        pTD = GetComponentInParent<PlayerTakeDamage>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            Transform enemyTransform = collision.transform.parent;
            Animator enemyAnimator = collision.GetComponentInParent<Animator>();
            EnemyStat enemyStat = collision.GetComponentInParent<EnemyStat>();

            Vector2 direction = (target.position - enemyTransform.position).normalized;
            enemyStat.lastDirection = direction;
            if (!oneTimeSaveScale)
            {
                scaleSave = Mathf.Abs(enemyTransform.localScale.x) * (enemyStat.inverseLook ? -1 : 1);
                oneTimeSaveScale = true;
            }

            if (enemyStat.haveAnim)
            {
                enemyAnimator.SetBool("IsMoving", true);
            }

            if (direction.x >= 0)
                enemyTransform.localScale = new Vector3(scaleSave, enemyTransform.localScale.y, enemyTransform.localScale.z);
            else if (direction.x < 0)
                enemyTransform.localScale = new Vector3(-scaleSave, enemyTransform.localScale.y, enemyTransform.localScale.z);


            enemyTransform.position = Vector2.MoveTowards(enemyTransform.position, target.position, enemyStat.moveSpeed * Time.deltaTime);

        }
        if (collision.gameObject.layer == 16 && !pS.waitForAttack)
        {
            Enemy enemy = collision.gameObject.GetComponentInParent<Enemy>();
            EnemyStat enemyStat = collision.gameObject.GetComponentInParent<EnemyStat>();

            Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;
            Vector2 ifKnockBackIsZero = Vector2.down;

            if (knockbackDirection == Vector2.zero)
            {
                pS.rb.AddForce(ifKnockBackIsZero * enemyStat.propulsionForce, ForceMode2D.Impulse);
            }
            else
            {
                pS.rb.AddForce(knockbackDirection * enemyStat.propulsionForce, ForceMode2D.Impulse);
            }

            StartCoroutine(WaitToAttack());

            pTD.TakeDamage(enemyStat.damage);

            if (pS.ice)
            {
                Vector2 forceDirection = pS.lastMoveDirection;
                if (forceDirection == Vector2.zero)
                {
                    forceDirection = Vector2.right;
                }

                enemy.rb.AddForce(forceDirection * propulsionForce, ForceMode2D.Impulse);
                StartCoroutine(enemy.TakeDamage(iceDamage));
            }

            if (enemyStat.haveAnim)
            {
                Animator enemyAnimator = collision.gameObject.GetComponentInParent<Animator>();
                enemyAnimator.SetTrigger("Attack");
            }
        }
        if (collision.gameObject.layer == 12)
        {
            pTD.TakeDamage(5);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        oneTimeSaveScale = false;
        if (collision.gameObject.layer == 9)
        {
            Animator enemyAnimator = collision.GetComponentInParent<Animator>();
            EnemyStat enemyStat = collision.GetComponentInParent<EnemyStat>();
            if (enemyStat.haveAnim)
            {
                enemyAnimator.SetBool("IsMoving", false);
            }
        }
    }

    IEnumerator WaitToAttack()
    {
        pS.waitForAttack = true;
        yield return new WaitForSeconds(waitTimeForAttack);
        pS.waitForAttack = false;
    }
}