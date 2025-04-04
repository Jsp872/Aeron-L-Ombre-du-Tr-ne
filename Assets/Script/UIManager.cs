using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class statUIManager : MonoBehaviour
{
    [SerializeField] Slider hpBar;
    [SerializeField] Slider staminaBar;
    [SerializeField] Slider manaBar;

    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject optionPanel;
    [SerializeField] GameObject controlPanel;

    [SerializeField] PlayerController player;
    [SerializeField] PlayerMagic playerMagic;

    [SerializeField] GameObject UiMenuMagic;

    private void OnEnable()
    {
        PlayerController.OnLifeChanged += OnHpChange;
        PlayerController.OnStaminaChanged += OnStaminaChange;
        PlayerController.OnManaChanged += OnManaChange;
        PlayerMagic.OnManaChanged += OnManaChange;
    }

    private void OnDisable()
    {
        PlayerController.OnLifeChanged -= OnHpChange;
        PlayerController.OnStaminaChanged -= OnStaminaChange;
        PlayerController.OnManaChanged -= OnManaChange;
        PlayerMagic.OnManaChanged -= OnManaChange;
    }

    public void OnReplayButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
    }

    private void OnHpChange(float newLife)
    {
        hpBar.maxValue = player.maxLife;
        hpBar.value = newLife;
    }

    private void OnStaminaChange(float newStamina)
    {
        staminaBar.maxValue = player.maxStamina;
        staminaBar.value = newStamina;
    }

    private void OnManaChange(float newMana)
    {
        manaBar.maxValue = player.maxMana;
        manaBar.value = newMana;
    }

    public void OnContinueButton()
    {
        player.isPause = false;
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    public void OnOptionButton()
    {
        optionPanel.SetActive(true);
    }

    public void OnMenuButton()
    {
        player.isPause = false;
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void OnCloseMagicMenuButton()
    {
        UiMenuMagic.SetActive(false);
    }

    public void OnControlButton()
    {
        controlPanel.SetActive(true);
    }

    public void OnCloseControlButton()
    {
        controlPanel.SetActive(false);
    }
}
