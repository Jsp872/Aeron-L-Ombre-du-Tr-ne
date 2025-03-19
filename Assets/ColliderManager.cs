using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class ColliderManager : MonoBehaviour
{

    [SerializeField] TriggerManager triggerManager;
    [SerializeField] float propulsionForce = 0.001f;

    PlayerController player;

    private void Awake()
    {
        player = GetComponent<PlayerController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
            Animator enemyAnimator = collision.gameObject.GetComponent<Animator>();
            enemyAnimator.SetTrigger("Attack");
            triggerManager.touch = true;
            player.rb.linearVelocity += collision.transform.forward * propulsionForce;
            player.TakeDamage(1);
        }


    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 7)
            triggerManager.touch = false;
    }
}
