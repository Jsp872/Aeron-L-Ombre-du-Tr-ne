using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject optionPanel;
    [SerializeField] GameObject controlPanel;
    [SerializeField] GameObject UiMenuMagic;
    [SerializeField] GameObject Plain;
    [SerializeField] List<GameObject> Dungeon = new List<GameObject>();

    [Header("PlayerReference")]
    [SerializeField] PlayerInteract pI;
    [SerializeField] PlayerStat pS;

    [Header("RespawnValue")]
    [SerializeField] Vector2 respawnPosition;

    public void OnReplayButton()
    {
        Time.timeScale = 1;
        foreach (GameObject dungeon in Dungeon)
        {
            dungeon.SetActive(false);
        }
        pS.gameObject.SetActive(true);
        pS.gameObject.transform.position = respawnPosition;
        pS.UIDefeat.SetActive(false);
        pI.Heal();
        Plain.SetActive(true);
        pS.rb.linearVelocity = Vector2.zero;
        pS.spriteRenderer.enabled = true;
    }

    public void OnContinueButton()
    {
        pS.isPause = false;
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    public void OnOptionButton()
    {
        optionPanel.SetActive(true);
    }

    public void OnMenuButton()
    {
        pS.isPause = false;
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