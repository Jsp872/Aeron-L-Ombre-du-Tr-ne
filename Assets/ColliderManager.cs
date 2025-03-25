using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ColliderManager : MonoBehaviour
{
    [SerializeField] TriggerManager triggerManager;
    [SerializeField] float propulsionForce = 5f;

    PlayerController player;
    bool waitForAttack;

    private void Awake()
    {
        player = GetComponent<PlayerController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7 && !waitForAttack)
        {
            triggerManager.touch = true;
            Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;

            player.rb.AddForce(knockbackDirection * propulsionForce, ForceMode2D.Impulse);
            StartCoroutine(WaitToAttack());
            player.TakeDamage(1);
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
