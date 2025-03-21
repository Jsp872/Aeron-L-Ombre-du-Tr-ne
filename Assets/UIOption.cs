using UnityEngine;
using UnityEngine.UI;

public class UIOption : MonoBehaviour
{
    [SerializeField] GameObject videoPanel;
    [SerializeField] GameObject audioPanel;
    [SerializeField] Toggle toggleForFullScreen;
    [SerializeField] Toggle toggleForMuteSound;
    [SerializeField] Slider soundVolume;

    [SerializeField] AudioSource audioSource;

    private void Awake()
    {
        bool isFullScreen = PlayerPrefs.GetInt("FullScreen", Screen.fullScreen ? 1 : 0) == 1;
        bool isMuted = PlayerPrefs.GetInt("Muted", audioSource.mute ? 1 : 0) == 1;
        float volume = PlayerPrefs.GetFloat("Volume", 1);

        Screen.fullScreen = isFullScreen;
        audioSource.mute = isMuted;
        audioSource.volume = volume;

        toggleForFullScreen.isOn = isFullScreen;
        toggleForMuteSound.isOn = isMuted;
        soundVolume.value = volume;
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void OnVideoButton()
    {
        videoPanel.SetActive(true);
        audioPanel.SetActive(false);
    }

    public void OnAudioButton()
    {
        videoPanel.SetActive(false);
        audioPanel.SetActive(true);
    }

    public void OnResumeButton()
    {
        gameObject.SetActive(false);
        videoPanel.SetActive(false);
        audioPanel.SetActive(false);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        PlayerPrefs.SetInt("FullScreen", isFullScreen ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void SetMuteSound(bool isMuted)
    {
        audioSource.mute = isMuted;
        PlayerPrefs.SetInt("Muted", isMuted ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void SetSoundVolume()
    {
        audioSource.volume = soundVolume.value;
        PlayerPrefs.SetFloat("Volume", soundVolume.value);
        PlayerPrefs.Save();
    }
}
