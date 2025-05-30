using System.Collections;
using UnityEngine;

public class Thunder : MonoBehaviour
{
    [SerializeField] private float delayForDespawn;
    [SerializeField] private float delayForDespawnAddIfEnemyTouch;
    [SerializeField] private float damage;

    private bool touchEnemy;
    private bool canDoDamage;
    private Vector3 farAway;

    void Start()
    {
        StartCoroutine(DestroyThunder());
        canDoDamage = true;
        farAway = new Vector3(1000, 1000, 0);
    } 
    
    IEnumerator DestroyThunder()
    {
        yield return new WaitForSeconds(delayForDespawn);
        if (touchEnemy)
        {
            transform.position = farAway;
            yield return new WaitForSeconds(delayForDespawnAddIfEnemyTouch);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            touchEnemy = true;
            GiveDamage(enemy);
        }
    }

    private void GiveDamage(Enemy enemy)
    { 
        if (canDoDamage)
        {
            StartCoroutine(enemy.TakeDamage(damage));
            canDoDamage = false;
        }
    }
}