using UnityEngine;

public class FixedCamera : MonoBehaviour
{
    [SerializeField] Transform player;
    void LateUpdate()
    {
        transform.position = player.position + new Vector3(0, 13, -4.8f);
    }

}
