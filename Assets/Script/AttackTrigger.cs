using System.Collections;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    [SerializeField] GameObject UIVictory;
    public int propulsionForce = 25;

    bool waitForAttack;
    PlayerController player;

    public float damage;

    private void Awake()
    {
        player = GetComponentInParent<PlayerController>();
        PlayerPrefs.SetFloat("damage", 1);
        damage = PlayerPrefs.GetFloat("damage", 1);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 7 && !waitForAttack)
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();

            Vector2 forceDirection = player.lastMoveDirection;
            if (forceDirection == Vector2.zero) forceDirection = Vector2.right;

            enemy.rb.AddForce(forceDirection * propulsionForce, ForceMode2D.Impulse);
            StartCoroutine(enemy.TakeDamage(damage));
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
