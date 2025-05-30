using System.Collections;
using UnityEngine;

public class FireBallEnemy : MonoBehaviour
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
        if (collision.gameObject.layer == 3)
        {
            PlayerTakeDamage player = collision.gameObject.GetComponent<PlayerTakeDamage>();
            StartCoroutine(GiveDamage(player));
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator GiveDamage(PlayerTakeDamage player)
    {
        player.TakeDamage(damage);
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