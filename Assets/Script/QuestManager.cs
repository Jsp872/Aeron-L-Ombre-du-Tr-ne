using System.Globalization;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public bool haveAActiveQuest;
    public int GetTheValueOfTheEnemyToKill;
    public int GetTheValueOfTheNumberOfEnemyToKill;
    public int GetTheValueOfTheReward;
    public float GetTheValueOfTheNumberOfReward;
    int numberOfEnemyKill;
    public GameObject questShow;

    [SerializeField] PlayerController pC;
    AttackTrigger aT;

    private void Awake()
    {
        aT = pC.gameObject.GetComponentInChildren<AttackTrigger>();
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
                            pC.maxLife += GetTheValueOfTheNumberOfReward;
                            pC.life += GetTheValueOfTheNumberOfReward;
                            break;
                        case 2:
                            pC.maxStamina += GetTheValueOfTheNumberOfReward;
                            pC.stamina += GetTheValueOfTheNumberOfReward;
                            break;
                        case 3:
                            pC.maxMana += GetTheValueOfTheNumberOfReward;
                            pC.mana += GetTheValueOfTheNumberOfReward;
                            break;
                        case 4:
                            PlayerPrefs.SetFloat("damage", PlayerPrefs.GetFloat("damage", 1) + GetTheValueOfTheNumberOfReward);
                            aT.damage = PlayerPrefs.GetFloat("damage", 1);
                            break;
                        case 5:
                            pC.moveSpeed += GetTheValueOfTheNumberOfReward;
                            break;
                    }
                    haveAActiveQuest = false;
                    numberOfEnemyKill = 0;
                    questShow.SetActive(false);
                }
            }
        }
    }
}
