using System.Collections;
using UnityEngine;

public class PlayerSprint : MonoBehaviour
{
    private PlayerStat pS;
    private Coroutine sprintCoroutine;

    private void Awake()
    {
        pS = GetComponent<PlayerStat>();
    }
    public void OnSprint()
    {
        if (pS.stamina > 10 && sprintCoroutine == null && !pS.isPause)
        {
            pS.isRunning = true;
            pS.moveSpeed = pS.stockMoveSpeed * 2;
            sprintCoroutine = StartCoroutine(DrainStamina());
            pS.animator.SetBool("Running", true);
        }
    }

    public void OnUnSprint()
    {
        pS.isRunning = false;
        pS.moveSpeed = pS.stockMoveSpeed;

        if (sprintCoroutine != null)
        {
            StopCoroutine(sprintCoroutine);
            sprintCoroutine = null;
        }
        pS.animator.SetBool("Running", false);
    }

    IEnumerator DrainStamina()
    {
        while (pS.isRunning && pS.stamina > 0)
        {
            pS.stamina = Mathf.Max(pS.stamina - 10, 0);
            pS.OnStaminaChange(pS.stamina);

            if (pS.stamina == 0)
                OnUnSprint();

            yield return new WaitForSeconds(1f);
        }
    }
}