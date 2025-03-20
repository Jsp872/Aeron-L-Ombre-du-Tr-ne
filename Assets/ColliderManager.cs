using System.Collections;
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 7 && !waitForAttack)
        {
            Animator enemyAnimator = collision.gameObject.GetComponent<Animator>();
            if (enemyAnimator != null)
            {
                enemyAnimator.SetTrigger("Attack");
            }

            triggerManager.touch = true;

            player.rb.AddForce(collision.contacts[0].normal * propulsionForce, ForceMode.Impulse);


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

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
            triggerManager.touch = false;
        }
    }
    
}
