using System.Collections;
using TMPro;
using UnityEngine;

public class InteractionTrigger : MonoBehaviour
{
    [SerializeField] private GameObject messageText;
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private QuestManager qM;
    [SerializeField] private GameObject textForShowE;

    [SerializeField] private Vector3 OnInteractableObject;

    private PlayerInteract pI;
    private PlayerMovement pM;
    private Message message;
    private TextMeshProUGUI text;

    private bool hasPressedE;
    private bool detection;
    private bool afterTextForQuest;
    private Vector2 tp;

    private enum InteractibleType { Message, Door, Heal }
    private InteractibleType interactibleType;

    private void Awake()
    {
        pI = GetComponentInParent<PlayerInteract>();
        text = messageText.GetComponentInChildren<TextMeshProUGUI>();
        pM = GetComponentInParent<PlayerMovement>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 14)
        {
            detection = true;
            Interactible interactible = collision.GetComponent<Interactible>();
            switch (interactible.interactibleType)
            {
                case Interactible.InteractibleType.Message:
                    Message(collision);
                    break;
                case Interactible.InteractibleType.Door:
                    Door(collision);
                    break;
                case Interactible.InteractibleType.Heal:
                    Heal(collision);
                    break;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 14)
        {
            detection = false;
            textForShowE.SetActive(false);
            Interactible interactible = collision.GetComponent<Interactible>();
            if (interactible.interactibleType == Interactible.InteractibleType.Message)
            {
                message.numberOfMessagesNow = 0;
                messageText.SetActive(false);
            }
        }
    }

    private void Message(Collider2D collision)
    {
        interactibleType = InteractibleType.Message;
        message = collision.gameObject.GetComponent<Message>();
        if (message.showE)
        {
            textForShowE.SetActive(true);
            textForShowE.transform.position = collision.transform.position + OnInteractableObject;
        }
        if (message.oneTime && !message.oneTimeActive)
        {
            pM.inputActionMove.action.Disable();

            pM.playerInput.actions["Dash"].Disable();

            messageText.SetActive(true);
            text.text = message.message[message.numberOfMessagesNow];
            message.numberOfMessagesNow++;
        }
    }

    private void Door(Collider2D collision)
    {
        interactibleType = InteractibleType.Door;
        textForShowE.SetActive(true);
        textForShowE.transform.position = collision.transform.position + OnInteractableObject;
        tp = collision.GetComponent<NewPositionOfPlayer>().dungeonPos;
    }

    private void Heal(Collider2D collision)
    {
        interactibleType = InteractibleType.Heal;
        textForShowE.SetActive(true);
        textForShowE.transform.position = collision.transform.position + OnInteractableObject;
    }

    public void Interact()
    {
        if (detection)
        {
            switch (interactibleType)
            {
                case InteractibleType.Message:
                    ShowTheMessage();
                    break;
                case InteractibleType.Door:
                    StartCoroutine(OpenDoor());
                    break;
                case InteractibleType.Heal:
                    HealPlayer();
                    break;
            }
        }
    }

    private void ShowTheMessage()
    {
        if (!hasPressedE && !message.oneTimeActive)
        {
            messageText.SetActive(true);

            if (!message.oneTime)
            {
                if (message.numberOfMessagesNow < message.numberOfMessages)
                {
                    text.text = message.message[message.numberOfMessagesNow];
                    message.numberOfMessagesNow++;
                }
                else if (qM.haveAActiveQuest && message.numberOfMessagesNow == message.numberOfMessages && !afterTextForQuest && message.haveAQuest)
                {
                    text.text = "Tu as déja une quete";
                    afterTextForQuest = true;
                }
                else if (message.numberOfMessagesNow == message.numberOfMessages)
                {
                    afterTextForQuest = false;
                    message.CheckLastMessage();
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
                    message.CheckLastMessage();
                    messageText.SetActive(false);
                    message.oneTimeActive = true;
                    pM.playerInput.actions["Dash"].Enable();
                    pM.inputActionMove.action.Enable();
                }
            }
        }
    }

    private IEnumerator OpenDoor()
    {
        loadingScreen.SetActive(true);
        pM.inputActionMove.action.Disable();
        yield return new WaitForSeconds(0.5f);
        loadingScreen.SetActive(false);
        pM.inputActionMove.action.Enable();
        pI.transform.position = tp;
    }

    public void HealPlayer()
    {
        pI.Heal();
    }
}