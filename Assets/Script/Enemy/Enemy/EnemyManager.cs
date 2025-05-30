using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [HideInInspector] public List<GameObject> enemies = new List<GameObject>();

    public void RespawnEnemy()
    {
        foreach (GameObject enemy in enemies)
        {
            EnemyStat enemyStat = enemy.GetComponent<EnemyStat>();
            enemy.SetActive(true);
            enemy.transform.position = enemyStat.initialPosition;
            enemyStat.life = enemyStat.maxLife;
            enemy.layer = 7;
            enemy.GetComponentInChildren<CircleCollider2D>().enabled = true;
            enemy.GetComponent<Enemy>().spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
            if (enemyStat.differentSprite)
            {
                SpriteRenderer[] otherSpriteRenderer = enemy.GetComponentsInChildren<SpriteRenderer>();
                foreach (SpriteRenderer sr in otherSpriteRenderer)
                {
                    sr.color = new Color(1f, 1f, 1f, 1f);
                }
            }
        }
    }
}
