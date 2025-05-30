using UnityEngine;
using UnityEngine.UI;

public class UiInGameManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] Slider hpBar;
    [SerializeField] Slider staminaBar;
    [SerializeField] Slider manaBar;

    [Header("PlayerReference")]
    [SerializeField] PlayerStat pS;

    private void OnEnable()
    {
        PlayerStat.OnLifeChanged += OnHpChange;
        PlayerStat.OnStaminaChanged += OnStaminaChange;
        PlayerStat.OnManaChanged += OnManaChange;
        ActivateMagic.OnManaChanged += OnManaChange;
    }

    private void OnDisable()
    {
        PlayerStat.OnLifeChanged -= OnHpChange;
        PlayerStat.OnStaminaChanged -= OnStaminaChange;
        PlayerStat.OnManaChanged -= OnManaChange;
        ActivateMagic.OnManaChanged -= OnManaChange;
    }

    private void OnHpChange(float newLife)
    {
        hpBar.maxValue = pS.maxLife;
        hpBar.value = newLife;
    }

    private void OnStaminaChange(float newStamina)
    {
        staminaBar.maxValue = pS.maxStamina;
        staminaBar.value = newStamina;
    }

    private void OnManaChange(float newMana)
    {
        manaBar.maxValue = pS.maxMana;
        manaBar.value = newMana;
    }
}