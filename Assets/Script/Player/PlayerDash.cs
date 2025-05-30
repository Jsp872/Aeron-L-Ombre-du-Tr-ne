using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    private PlayerStat pS;

    private void Awake()
    {
        pS = GetComponent<PlayerStat>();
    }
    public void OnDash()
    {
        if (pS.stamina >= 50 && !pS.isPause)
        {
            pS.stamina -= 50;
            pS.OnStaminaChange(pS.stamina);

            Vector2 dashDirection = pS.lastMoveDirection;
            if (dashDirection == Vector2.zero) dashDirection = Vector2.right;

            pS.rb.AddForce(dashDirection * pS.propulsionForce, ForceMode2D.Impulse);

            pS.animator.SetTrigger("Dash");
            StartCoroutine(TimeOfAttack());
            StartCoroutine(IsInvincible());
        }
    }

    private IEnumerator IsInvincible()
    {
        pS.isInvincible = true;
        pS.damage *= 3;
        yield return new WaitForSeconds(0.5f);
        pS.damage = pS.stockDamage;
        pS.isInvincible = false;
    }

    public IEnumerator TimeOfAttack()
    {
        pS.attack = true;
        yield return new WaitForSeconds(0.75f);
        pS.attack = false;
    }
}
