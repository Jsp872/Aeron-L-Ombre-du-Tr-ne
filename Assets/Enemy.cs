using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int moveSpeed = 2;
    [SerializeField] int life = 3;
    Animator animator;
    public Rigidbody2D rb;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    public void TakeDamage(int damage)
    {
        life -= damage;
        if (life <= 0)
        {
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        gameObject.layer = 8;
        gameObject.GetComponentInChildren<CircleCollider2D>().enabled = false;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

}