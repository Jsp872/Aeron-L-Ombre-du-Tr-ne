using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ThunderPrefab : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    CircleCollider2D circleCollider2D;
    Animator animator;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        StartCoroutine(Thunder());
    }
    IEnumerator Thunder()
    {
        yield return new WaitForSeconds(1f);
        animator.enabled = true;
        circleCollider2D.enabled = true;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
