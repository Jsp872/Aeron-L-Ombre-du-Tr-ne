using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject enemy;

    [SerializeField] int life = 3;
    Animator animator;
    public Rigidbody rb;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
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
        animator.SetTrigger("Die");
        gameObject.layer = 8;
        gameObject.GetComponentInChildren<SphereCollider>().enabled = false;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

}