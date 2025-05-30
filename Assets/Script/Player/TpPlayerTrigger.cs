using UnityEngine;
using UnityEngine.InputSystem;

public class TpPlayerTrigger : MonoBehaviour
{
    public FixedCamera fixedCamera;

    [HideInInspector] public bool anim;
    [HideInInspector] public PlayerInput playerInput;

    [SerializeField] GameObject playerAnimation;
    [SerializeField] private Vector2 playerPositionAfterBeCatch;
    [SerializeField] private Vector2 farAway;

    private PlayerStat pS;
    private PlayerSprint pSp;
    private ActivateMagic aM;
    private PlayerAnimation pA;

    private void Awake()
    {
        pA = playerAnimation.GetComponent<PlayerAnimation>();
        pS = GetComponentInParent<PlayerStat>();
        pSp = GetComponentInParent<PlayerSprint>();
        aM = GetComponentInParent<ActivateMagic>();
        playerInput = GetComponentInParent<PlayerInput>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            pS.transform.position = playerPositionAfterBeCatch;
        }
        if (collision.gameObject.layer == 13)
        {
            NewPositionOfPlayer newPos = collision.GetComponent<NewPositionOfPlayer>();
            OnAnimation(newPos);
        }
    }

    private void OnAnimation(NewPositionOfPlayer newPos)
    {
        pSp.OnUnSprint();
        pS.activateMagic = true;
        aM.OnActivateMagic();
        playerAnimation.SetActive(true);
        newPos.newMap.SetActive(true);
        pA.whereToLeave = newPos.whereToLeave;
        pA.audioForTheZone = newPos.audioForTheZone;
        playerAnimation.transform.position = pS.transform.position;
        playerAnimation.transform.rotation = pS.transform.rotation;
        pS.transform.position = farAway;
        anim = true;
        fixedCamera.enabled = false;
        playerInput.enabled = false;
    }
}