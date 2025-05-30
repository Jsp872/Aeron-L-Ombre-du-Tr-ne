using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapManager : MonoBehaviour
{
    public Tilemap tilemap;
    public Vector3Int[] doorTiles;

    [Header("Prison Door On Start Map")]
    [SerializeField] bool start;
    [SerializeField] float delayForOpenPrison;

    public void Start()
    {
        if (start)
        {
            StartCoroutine(OpenDoor());
        }
    }

    IEnumerator OpenDoor()
    {
        yield return new WaitForSeconds(delayForOpenPrison);
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
