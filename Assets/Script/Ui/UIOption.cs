using UnityEngine;

public class UIOption : MonoBehaviour
{
    [SerializeField] GameObject videoPanel;
    [SerializeField] GameObject audioPanel;

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
}