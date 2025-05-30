using UnityEngine;

public class FixedCamera : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Vector3 offset;
    void LateUpdate()
    {
        if (player == null)
            return;
        transform.position = player.position + offset;
    }

}
