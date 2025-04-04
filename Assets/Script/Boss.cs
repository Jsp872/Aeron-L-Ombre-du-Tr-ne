using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("Boss Settings")]
    public bool finalBoss;
    public bool falseFinalBoss;
    [SerializeField] TileMapManager tileMapManagerForEnteringCastle;

    [Header("For wind Boss")]
    [SerializeField] GameObject blockFireDungeon;
    [SerializeField] GameObject showWindPower;

    [Header("For Fire Boss")]
    [SerializeField] GameObject blockIceDungeon;
    [SerializeField] GameObject showFirePower;

    [Header("For Ice Boss")]
    [SerializeField] GameObject showIcePower;

    [Header("For Thunder Boss")]
    [SerializeField] GameObject firstCastleAnim;
    [SerializeField] GameObject secondCastleAnim;
    [SerializeField] GameObject showThunderPower;

    [Header("For false final Boss")]
    [SerializeField] GameObject blockThunderDungeon;

    [Header("For final Boss")]
    [SerializeField] GameObject victoryScreen;

    Animator animator;
    TileMapManager tileMapManager;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        tileMapManager = transform.parent.GetComponentInChildren<TileMapManager>();
    }

    public void Die()
    {

        if (finalBoss || falseFinalBoss)
        {
            animator.SetTrigger("Die");
            if (falseFinalBoss)
                blockThunderDungeon.SetActive(false);
            else
            {
                StartCoroutine(Victory());
            }

        }
        else
        {
            tileMapManagerForEnteringCastle.numberOfBossKilled++;
            if (tileMapManagerForEnteringCastle.numberOfBossKilled == 1)
            {
                blockFireDungeon.SetActive(false);
                showWindPower.SetActive(true);
            }
            else if (tileMapManagerForEnteringCastle.numberOfBossKilled == 2)
            {
                blockIceDungeon.SetActive(false);
                showFirePower.SetActive(true);
            }
            else if (tileMapManagerForEnteringCastle.numberOfBossKilled == 3)
            {
                tileMapManagerForEnteringCastle.OpenBossDoor();
                showIcePower.SetActive(true);
            }
            else if (tileMapManagerForEnteringCastle.numberOfBossKilled == 4)
            {
                firstCastleAnim.SetActive(false);
                secondCastleAnim.SetActive(true);
                showThunderPower.SetActive(true);
            }
        }
            tileMapManager.OpenBossDoor();
    }

    public IEnumerator Victory()
    {
        yield return new WaitForSeconds(1f);
        victoryScreen.SetActive(true);
        Time.timeScale = 0;
    }
}
