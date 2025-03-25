using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapManager : MonoBehaviour
{
    public Tilemap tilemap;
    public Vector3Int[] doorTiles;


    
    public void Start()
    {
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
}
