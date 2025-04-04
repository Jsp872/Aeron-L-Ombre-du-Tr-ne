using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.InputSystem;

public class TriggerManager : MonoBehaviour
{
    public bool anim;
    Transform target;
    [SerializeField] Transform playerTransform;
    public bool touch;

    bool oneTimeSaveScale = false;
    float scaleSave;

    [SerializeField] GameObject player;
    [SerializeField] GameObject messageText;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] GameObject playerAnimation;
    [SerializeField] PlayerAnimation pA;


    public FixedCamera fixedCamera;
    public PlayerInput playerInput;

    PlayerController pC;
    PlayerMagic pM;
    [SerializeField] QuestManager qM;
    private bool hasPressedE = false;

    bool detection;
    bool afterTextForQuest;

    Message message;
    private void Awake()
    {
        target = GetComponent<Transform>();
        playerInput = playerTransform.GetComponent<PlayerInput>();
        pC = playerTransform.GetComponent<PlayerController>();
        pM = playerTransform.GetComponent<PlayerMagic>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            Transform enemyTransform = collision.transform.parent;
            Animator enemyAnimator = collision.GetComponentInParent<Animator>();
            Enemy enemy = collision.GetComponentInParent<Enemy>();
            Boss boss = collision.GetComponentInParent<Boss>();

            Vector2 direction = (target.position - enemyTransform.position).normalized;

            if (!oneTimeSaveScale)
            {
                if (!enemy.inverseLook)
                {
                    if (enemyTransform.localScale.x > 0)
                        scaleSave = enemyTransform.localScale.x;
                    else if (enemyTransform.localScale.x < 0)
                        scaleSave = -enemyTransform.localScale.x;
                }
                else
                {
                    if (enemyTransform.localScale.x > 0)
                        scaleSave = -enemyTransform.localScale.x;
                    else if (enemyTransform.localScale.x < 0)
                        scaleSave = enemyTransform.localScale.x;
                }

                    oneTimeSaveScale = true;
            }
            if (enemy.bossCheck)
            {
                if (boss.falseFinalBoss || boss.finalBoss)
                    enemyAnimator.SetBool("IsMoving", true);
            }

            if (direction.x >= 0)
                    enemyTransform.localScale = new Vector3(scaleSave, enemyTransform.localScale.y, enemyTransform.localScale.z);
            else if (direction.x < 0)
                    enemyTransform.localScale = new Vector3(-scaleSave, enemyTransform.localScale.y, enemyTransform.localScale.z);
                

            if (!touch)
            {
                enemyTransform.position = Vector2.MoveTowards(enemyTransform.position, target.position, enemy.moveSpeed * Time.deltaTime);
            }
        }
        if (collision.gameObject.layer == 10)
        {
            playerTransform.transform.position = new Vector2(0, 15);
        }
        if (collision.gameObject.layer == 13)
        {
            pC.OnUnSprint();
            pM.activate = true;
            pM.OnActivateMagic();
            playerAnimation.SetActive(true);
            NewPositionOfPlayer newPos = collision.GetComponent<NewPositionOfPlayer>();
            newPos.newMap.SetActive(true);
            pA.whereToLeave = newPos.whereToLeave;
            playerAnimation.transform.position = playerTransform.position;
            playerAnimation.transform.rotation = playerTransform.rotation;
            player.transform.position = new Vector2(-33.9f, 96.48f);
            anim = true;
            fixedCamera.enabled = false;
            playerInput.enabled = false;
        }
        
    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        oneTimeSaveScale = false;
        if (collision.gameObject.layer == 14)
        {
            message.numberOfMessagesNow = 0;
            detection = false;
            messageText.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 14)
        {
            message = collision.gameObject.GetComponent<Message>();
            detection = true;
            if (message.oneTime && !message.oneTimeActive)
            {
                pC.inputActionMove.action.Disable();

                messageText.SetActive(true);
                text.text = message.message[message.numberOfMessagesNow];
                message.numberOfMessagesNow++;
            }
        }
    }

    private void Update()
    {
        if (detection)
        {
            if (Input.GetKeyDown(KeyCode.E) && !hasPressedE && !message.oneTimeActive)
            {

                messageText.SetActive(true);

                if (!message.oneTime)
                {
                    if (message.numberOfMessagesNow < message.numberOfMessages)
                    {
                        text.text = message.message[message.numberOfMessagesNow];
                        message.numberOfMessagesNow++;
                    }
                    else if (qM.haveAActiveQuest && message.numberOfMessagesNow == message.numberOfMessages && !afterTextForQuest)
                    {
                        text.text = "Tu as déja une quete";
                        afterTextForQuest = true;
                    }
                    else if (message.numberOfMessagesNow == message.numberOfMessages)
                    {
                        afterTextForQuest = false;
                        message.checkLastMessage();
                        message.numberOfMessagesNow = 0;
                        messageText.SetActive(false);
                    }
                }
                else if (!message.oneTimeActive)
                {
                    if (message.numberOfMessagesNow < message.numberOfMessages)
                    {
                        text.text = message.message[message.numberOfMessagesNow];
                        message.numberOfMessagesNow++;
                    }
                    else if (message.numberOfMessagesNow == message.numberOfMessages)
                    {
                        message.checkLastMessage();
                        messageText.SetActive(false);
                        message.oneTimeActive = true;
                        pC.inputActionMove.action.Enable();
                    }
                }
            }

        }
    }
}
