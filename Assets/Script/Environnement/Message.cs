using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Interactible))]
public class Message : MonoBehaviour
{
    [Header("HaveQuest")]
    public bool haveAQuest;
    [SerializeField] string messageForQuest;
    [SerializeField] string rewardForQuest;
    public int ValueOfTheEnemyToKill;
    public int ValueOfTheNumberOfEnemyToKill;
    public int ValueOfReward;
    public float numberOfReward;

    [Header("HaveAMessage")]
    public List<string> message;
    public bool oneTime;
    public bool showE;

    [HideInInspector] public int numberOfMessages;
    [HideInInspector] public int numberOfMessagesNow = 0;
    [HideInInspector] public bool oneTimeActive;

    private QuestManager qM;
    private GameObject questShow;

    private void Awake()
    {
        questShow = GetComponentInParent<QuestManager>().questShow;
        qM = GetComponentInParent<QuestManager>();
        numberOfMessages = message.Count;
    }

    public void CheckLastMessage()
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
            textReward.gameObject.SetActive(false);

            TextMeshProUGUI textEnemy = questShow.GetComponentInChildren<TextMeshProUGUI>();
            textEnemy.text = $"{qM.numberOfEnemyKill} / {ValueOfTheNumberOfEnemyToKill}";
            textEnemy.gameObject.SetActive(true);
            textQuest.gameObject.SetActive(true);
            textReward.gameObject.SetActive(true);

            qM.GetTheValueOfTheEnemyToKill = ValueOfTheEnemyToKill;
            qM.GetTheValueOfTheNumberOfEnemyToKill = ValueOfTheNumberOfEnemyToKill;
            qM.GetTheValueOfTheReward = ValueOfReward;
            qM.GetTheValueOfTheNumberOfReward = numberOfReward;
        }
    }
}