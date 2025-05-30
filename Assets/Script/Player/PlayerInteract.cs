using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private PlayerStat pS;
    private InteractionTrigger iT;

    private void Awake()
    {
        pS = GetComponent<PlayerStat>();
        iT = GetComponentInChildren<InteractionTrigger>();
    }
    public void OnPause()
    {
        pS.isPause = true;
        Time.timeScale = 0;
        pS.UiPause.SetActive(true);
    }
    public void OnInteract()
    {
        iT.Interact();
    }

    public void Heal()
    {
        pS.life = pS.maxLife;
        pS.stamina = pS.maxStamina;
        pS.mana = pS.maxMana;
        pS.OnLifeChange(pS.life);
        pS.OnStaminaChange(pS.stamina);
        pS.OnManaChange(pS.mana);
    }
}
