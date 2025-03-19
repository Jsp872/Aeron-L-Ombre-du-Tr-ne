using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public void OnReplayButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("SampleScene");
    }
}
