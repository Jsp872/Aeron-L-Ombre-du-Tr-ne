using System.Collections;
using UnityEngine;

public class IceSpike : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private PolygonCollider2D polygonCollider2D;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();
        StartCoroutine(IceBossAttack2());
    }

    private IEnumerator IceBossAttack2()
    {
        yield return new WaitForSeconds(1.5f);
        spriteRenderer.color = Color.cyan;
        polygonCollider2D.enabled = true;
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
