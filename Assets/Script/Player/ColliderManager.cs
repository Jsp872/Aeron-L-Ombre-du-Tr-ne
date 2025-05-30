using System.Collections;
using UnityEngine;

public class ColliderManager : MonoBehaviour
{
    private EnemyDetectorTrigger triggerManager;
    [SerializeField] float propulsionForce;
    [SerializeField] int environnementDamage;

    private PlayerStat pS;
    private PlayerTakeDamage pTD;

    private void Awake()
    {
        triggerManager = GetComponentInChildren<EnemyDetectorTrigger>();
        pS = GetComponent<PlayerStat>();
        pTD = GetComponent<PlayerTakeDamage>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {


        if (collision.gameObject.layer == 12)
        {
            EnvironnementDamage(collision.gameObject);
        }
        if (collision.gameObject.layer == 15)
        {
            BlockDungeon blockDungeon = collision.gameObject.GetComponent<BlockDungeon>();
            if (blockDungeon.doDamage)
            {
                EnvironnementDamage(collision.gameObject);
            }
        }
    }

    private void EnvironnementDamage(GameObject collision)
    {
        Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;

        pS.rb.AddForce(knockbackDirection * propulsionForce, ForceMode2D.Impulse);
        pTD.TakeDamage(environnementDamage);
    }
}