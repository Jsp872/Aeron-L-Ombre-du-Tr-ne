using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class statUIManager : MonoBehaviour
{
    [SerializeField] Slider hpBar;
    [SerializeField] Slider staminaBar;
    [SerializeField] Slider manaBar;

    private void OnEnable()
    {
        PlayerController.OnLifeChanged += OnHpChange;
        PlayerController.OnStaminaChanged += OnStaminaChange;
        PlayerController.OnManaChanged += OnManaChange;
    }

    private void OnDisable()
    {
        PlayerController.OnLifeChanged -= OnHpChange;
        PlayerController.OnStaminaChanged -= OnStaminaChange;
        PlayerController.OnManaChanged -= OnManaChange;
    }

    public void OnReplayButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("SampleScene");
    }

    private void OnHpChange(int newLife)
    {
        hpBar.value = newLife;
    }

    private void OnStaminaChange(int newStamina)
    {
        staminaBar.value = newStamina;
    }

    private void OnManaChange(int newMana)
    {
        manaBar.value = newMana;
    }
}
