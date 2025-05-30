using System.Collections;
using UnityEngine;

public class Lava : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(WaitForDestroy());
    }

    IEnumerator WaitForDestroy()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
