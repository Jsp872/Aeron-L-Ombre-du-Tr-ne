using System.Collections;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    [SerializeField] GameObject UIVictory;
    int propulsionForce = 25;

    bool waitForAttack;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7 && !waitForAttack)
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.rb.AddForce(transform.forward * propulsionForce, ForceMode.Impulse);
            enemy.TakeDamage(1);
            StartCoroutine(WaitToAttack());
        }

    }

    IEnumerator WaitToAttack()
    {
        waitForAttack = true;
        yield return new WaitForSeconds(0.5f);
        waitForAttack = false;
    }
}
