using UnityEngine;
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
    [SerializeField] GameObject playerAnimation;
    [SerializeField] PlayerAnimation pA;


    public FixedCamera fixedCamera;
    public PlayerInput playerInput;

    PlayerController pC;
    PlayerMagic pM;

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

            Vector2 direction = (target.position - enemyTransform.position).normalized;

            if (!oneTimeSaveScale)
            {
                if (enemyTransform.localScale.x > 0)
                    scaleSave = enemyTransform.localScale.x;
                else if (enemyTransform.localScale.x < 0)
                    scaleSave = -enemyTransform.localScale.x;
                oneTimeSaveScale = true;
            }
            if (enemy.finalBoss || enemy.falseFinalBoss)
                enemyAnimator.SetBool("IsMoving", true);
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
    }


}
