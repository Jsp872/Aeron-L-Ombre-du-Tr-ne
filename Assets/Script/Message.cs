using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Message : MonoBehaviour
{
    [Header("HaveQuest")]
    [SerializeField] bool haveAQuest;
    public int ValueOfTheEnemyToKill;
    public int ValueOfTheNumberOfEnemyToKill;
    public int ValueOfReward;
    public float numberOfReward;
    [SerializeField] string messageForQuest;
    [SerializeField] string rewardForQuest;
    GameObject questShow;

    [Header("HaveAMessage")]
    public List<string> message;
    public bool oneTime;

    [HideInInspector] public int numberOfMessages;
    [HideInInspector] public int numberOfMessagesNow = 0;
    QuestManager qM;
    [HideInInspector] public bool oneTimeActive;

    private void Awake()
    {
        questShow = GetComponentInParent<QuestManager>().questShow;
        qM = GetComponentInParent<QuestManager>();
        numberOfMessages = message.Count;
    }

    public void checkLastMessage()
    {
        if (numberOfMessagesNow == numberOfMessages && haveAQuest && !qM.haveAActiveQuest)
        {
            qM.haveAActiveQuest = true;
            questShow.SetActive(true);
            TextMeshProUGUI textQuest = questShow.GetComponentInChildren<TextMeshProUGUI>();
            textQuest.text = messageForQuest;
            textQuest.gameObject.SetActive(false);
            TextMeshProUGUI textReward = questShow.GetComponentInChildren<TextMeshProUGUI>();
            textReward.text = rewardForQuest;
            textQuest.gameObject.SetActive(true);
            qM.GetTheValueOfTheEnemyToKill = ValueOfTheEnemyToKill;
            qM.GetTheValueOfTheNumberOfEnemyToKill = ValueOfTheNumberOfEnemyToKill;
            qM.GetTheValueOfTheReward = ValueOfReward;
            qM.GetTheValueOfTheNumberOfReward = numberOfReward;
        }
    }
}
