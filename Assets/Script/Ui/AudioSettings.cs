using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] Toggle toggleForMuteSound;
    [SerializeField] Slider soundVolume;
    [SerializeField] AudioSource audioSource;

    private void Awake()
    {
        bool isMuted = PlayerPrefs.GetInt("Muted", audioSource.mute ? 1 : 0) == 1;
        float volume = PlayerPrefs.GetFloat("Volume", 1);

        audioSource.mute = isMuted;
        audioSource.volume = volume;

        toggleForMuteSound.isOn = isMuted;
        soundVolume.value = volume;
    }

    public void SetMuteSound(bool isMuted)
    {
        isMuted = toggleForMuteSound.isOn;
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