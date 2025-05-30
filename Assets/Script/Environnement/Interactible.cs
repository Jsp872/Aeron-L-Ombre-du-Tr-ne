using UnityEngine;

public class Interactible : MonoBehaviour
{
    public enum InteractibleType
    {
        Message,
        Door,
        Heal,
    }

    public InteractibleType interactibleType;
}
