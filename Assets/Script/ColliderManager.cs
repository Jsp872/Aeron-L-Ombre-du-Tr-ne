using System.Collections;
using UnityEngine;

public class ColliderManager : MonoBehaviour
{
    [SerializeField] TriggerManager triggerManager;
    [SerializeField] float propulsionForce = 5f;

    PlayerController player;
    public bool waitForAttack;

    private void Awake()
    {
        player = GetComponent<PlayerController>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7 && !waitForAttack)
        {
            triggerManager.touch = true;
            Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;
            Vector2 ifKnockBackIsZero = new Vector2(0, -1);
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            Boss boss = collision.gameObject.GetComponent<Boss>();
            if (knockbackDirection == new Vector2(0, 0))
            {
                player.rb.AddForce(ifKnockBackIsZero * enemy.propulsionForce, ForceMode2D.Impulse);
            }
            else 
                player.rb.AddForce(knockbackDirection * enemy.propulsionForce, ForceMode2D.Impulse);
            StartCoroutine(WaitToAttack());
            player.TakeDamage(enemy.damage);
            if (enemy.bossCheck)
            {
                if (boss.falseFinalBoss || boss.finalBoss)
                {
                    Animator bossAnimator = collision.gameObject.GetComponent<Animator>();
                    bossAnimator.SetTrigger("Attack");
                }
            }
        }
        if (collision.gameObject.layer == 12)
        {
            Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;

            player.rb.AddForce(knockbackDirection * propulsionForce, ForceMode2D.Impulse);
            player.TakeDamage(5);
        }

    }

    IEnumerator WaitToAttack()
    {
        waitForAttack = true;
        yield return new WaitForSeconds(0.25f);
        waitForAttack = false;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            triggerManager.touch = false;
        }
    }
}
