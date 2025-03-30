using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class TileMapManager : MonoBehaviour
{
    public Tilemap tilemap;
    public Vector3Int[] doorTiles;

    [SerializeField] bool start;


    public void Start()
    {
        if (start)
           StartCoroutine(OpenDoor());
    }

    IEnumerator OpenDoor()
    {
        yield return new WaitForSeconds(3);
        foreach (Vector3Int pos in doorTiles)
        {
            tilemap.SetTile(pos, null);
        }
    }

    public void OpenBossDoor()
    {
        foreach (Vector3Int pos in doorTiles)
        {
            tilemap.SetTile(pos, null);
        }
    }
}
