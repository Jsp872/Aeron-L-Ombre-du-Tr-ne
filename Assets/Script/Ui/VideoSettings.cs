using UnityEngine;
using UnityEngine.UI;

public class VideoSettings : MonoBehaviour
{
    [SerializeField] Toggle toggleForFullScreen;

    private void Awake()
    {
        bool isFullScreen = PlayerPrefs.GetInt("FullScreen", Screen.fullScreen ? 1 : 0) == 1;
        Screen.fullScreen = isFullScreen;
        toggleForFullScreen.isOn = isFullScreen;
    }

    public void SetFullScreen(bool isFullScreen)
    {
        isFullScreen = toggleForFullScreen.isOn;
        Screen.fullScreen = isFullScreen;
        PlayerPrefs.SetInt("FullScreen", isFullScreen ? 1 : 0);
        PlayerPrefs.Save();
    }
}