using System.Collections;
using UnityEngine;

public class PlayerTakeDamage : MonoBehaviour
{


    private PlayerStat pS;

    private void Awake()
    {
        pS = GetComponent<PlayerStat>();
    }
    public void TakeDamage(float damage)
    {
        if (pS.isInvincible)
            return;
        if (pS.ice)
            damage /= 2;

        if (pS.isBlocking && pS.stamina >= 30)
        {
            damage /= 2;
            pS.stamina -= 30;
            pS.OnStaminaChange(pS.stamina);
        }

        int finalDamage = Mathf.FloorToInt(damage);
        if (finalDamage > 0)
        {
            if (pS.ice) pS.particleForIce.Play();
            pS.isInvincible = true;
            pS.life -= finalDamage;
            pS.animator.SetTrigger("Hit");
            StartCoroutine(HitAnim());
            pS.OnLifeChange(pS.life);
        }


        if (pS.life <= 0)
        {
            pS.spriteRenderer.enabled = false;
            pS.doorHouse.SetActive(true);
            Time.timeScale = 0;
            pS.UIDefeat.SetActive(true);
        }
    }


    IEnumerator HitAnim()
    {
        int count = 0;
        int maxCount = 3;
        while (count < maxCount)
        {
            pS.spriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.1f);
            pS.spriteRenderer.enabled = true;
            yield return new WaitForSeconds(0.1f);
            count++;
        }
        pS.isInvincible = false;
    }
}
