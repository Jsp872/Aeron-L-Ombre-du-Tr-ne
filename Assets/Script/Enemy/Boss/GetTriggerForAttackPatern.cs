using UnityEngine;

public class GetTriggerForAttackPatern : MonoBehaviour
{
    private Collider2D[] colliders;
    private SpriteRenderer[] spriteRenderers;
    private Animator[] animators;

    void Awake()
    {
        colliders = GetComponentsInChildren<Collider2D>();
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        animators = GetComponentsInChildren<Animator>();
    }

    public void SetActive(bool active)
    {
        foreach (var col in colliders)
            col.enabled = active;
    }

    public void ResetAttack()
    {
        foreach (var spriteRenderer in spriteRenderers)
        {
            spriteRenderer.color = Color.white;
        }
        SetActive(false);
        gameObject.SetActive(false);
    }

    public void ChangeColorInCyan()
    {
        foreach (var spriteRenderer in spriteRenderers)
        {
            spriteRenderer.color = Color.cyan;
        }   
    }

    public void ActiveAnimator()
    {
        foreach (var animator in animators)
        {
            animator.enabled = true;
        }
    }

    public void ResetAttackThunder()
    {
        foreach (var animator in animators)
        {
            animator.enabled = false;
        }
        SetActive(false);
        gameObject.SetActive(false);
    }
}
