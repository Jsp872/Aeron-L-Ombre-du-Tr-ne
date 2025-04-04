using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManagerForMainMenu : MonoBehaviour
{
    [SerializeField] GameObject optionPanel;
    [SerializeField] GameObject controlPanel;
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void OnOptionButton()
    {
        optionPanel.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
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
