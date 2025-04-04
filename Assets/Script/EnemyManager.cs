using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<GameObject> enemies = new List<GameObject>();

    public void RespawnEnemy()
    {
        foreach (var enemy in enemies)
        {
            Enemy enemyStat = enemy.GetComponent<Enemy>();
            enemy.gameObject.SetActive(true);
            enemy.transform.position = enemyStat.initialPosition;
            enemyStat.life = enemyStat.maxLife;
            enemy.layer = 7;
            enemy.GetComponentInChildren<CircleCollider2D>().enabled = true;
        }
    }
}
