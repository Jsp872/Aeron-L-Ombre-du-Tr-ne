using System.Collections;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField] private float lifeTimeAfterTouchAEnemy;
    [SerializeField] private float lifeTime;

    private Rigidbody2D rb;
    private Vector3 farAway;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        rb.linearVelocity = transform.right * speed;
        farAway = new Vector3(1000, 1000, 0);
        StartCoroutine(WaitForDestroy());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            StartCoroutine(GiveDamage(enemy));
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator GiveDamage(Enemy enemy)
    {
        StartCoroutine(enemy.TakeDamage(damage));
        transform.position = farAway;
        yield return new WaitForSeconds(lifeTimeAfterTouchAEnemy);
        Destroy(gameObject);
    }

    private IEnumerator WaitForDestroy()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
