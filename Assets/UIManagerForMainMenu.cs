using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManagerForMainMenu : MonoBehaviour
{
    [SerializeField] GameObject optionPanel;
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
}
