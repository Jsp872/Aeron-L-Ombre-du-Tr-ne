using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public GameObject questShow;

    [HideInInspector] public bool haveAActiveQuest;
    [HideInInspector] public int GetTheValueOfTheEnemyToKill;
    [HideInInspector] public int GetTheValueOfTheNumberOfEnemyToKill;
    [HideInInspector] public int GetTheValueOfTheReward;
    [HideInInspector] public float GetTheValueOfTheNumberOfReward;
    [HideInInspector] public int numberOfEnemyKill;
    [HideInInspector] public TextMeshProUGUI messageOfNumberEnemyKill;

    [Header("Reference")]
    [SerializeField] private PlayerStat pS;
    [SerializeField] private GameObject affirmRemoveQuest;
    [SerializeField] private GameObject messageOfNumberEnemyKillObject;

    private void Awake()
    {
        messageOfNumberEnemyKill = messageOfNumberEnemyKillObject.GetComponent<TextMeshProUGUI>();
    }

    public void CheckEnemyForTheQuest(int valueOfTheEnemy)
    {
        if (haveAActiveQuest)
        {
            if (valueOfTheEnemy == GetTheValueOfTheEnemyToKill)
            {
                numberOfEnemyKill++;
                if (numberOfEnemyKill == GetTheValueOfTheNumberOfEnemyToKill)
                {
                    switch (GetTheValueOfTheReward)
                    {
                        case 1:
                            pS.maxLife += GetTheValueOfTheNumberOfReward;
                            pS.life += GetTheValueOfTheNumberOfReward;
                            break;
                        case 2:
                            pS.maxStamina += GetTheValueOfTheNumberOfReward;
                            pS.stamina += GetTheValueOfTheNumberOfReward;
                            break;
                        case 3:
                            pS.maxMana += GetTheValueOfTheNumberOfReward;
                            pS.mana += GetTheValueOfTheNumberOfReward;
                            break;
                        case 4:
                            pS.stockDamage += GetTheValueOfTheNumberOfReward;
                            break;
                        case 5:
                            pS.stockMoveSpeed += GetTheValueOfTheNumberOfReward;
                            pS.moveSpeed = pS.stockMoveSpeed;
                            break;
                    }
                    haveAActiveQuest = false;
                    numberOfEnemyKill = 0;
                    questShow.SetActive(false);
                }
            }
        }
    }

    public void AffirmRemoveQuest()
    {
        affirmRemoveQuest.SetActive(true);
    }

    public void RemoveQuest()
    {
        affirmRemoveQuest.SetActive(false);
        haveAActiveQuest = false;
        numberOfEnemyKill = 0;
        questShow.SetActive(false);
    }

    public void CancelRemoveQuest()
    {
        affirmRemoveQuest.SetActive(false);
    }
}