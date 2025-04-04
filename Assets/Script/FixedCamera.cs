using UnityEngine;

public class FixedCamera : MonoBehaviour
{
    [SerializeField] Transform player;
    void LateUpdate()
    {
        if (player == null)
            return;
        transform.position = player.position + new Vector3(0, 2.2f,-4.8f);
    }

}
