using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public InputActionReference inputActionMove;

    [HideInInspector] public PlayerInput playerInput;
    [HideInInspector] public Vector3 direction;


    private Animator animator;




    private int numberOfThunder;
    private PlayerStat pS;
    ActivateMagic aM;
    SelectMagic sM;
    private PlayerSprint pSp;
    [HideInInspector] public Vector2 lastOffset;
    [HideInInspector] public Vector2 lastSize;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        aM = GetComponentInChildren<ActivateMagic>();
        sM = GetComponent<SelectMagic>();
        pS = GetComponent<PlayerStat>();
        pSp = GetComponent<PlayerSprint>();
    }

    private void FixedUpdate()
    {
        direction = inputActionMove.action.ReadValue<Vector2>().normalized;
        if (direction != Vector3.zero)
            pS.lastMoveDirection = direction;

        Vector3 move = new Vector2(direction.x, direction.y) * pS.moveSpeed * Time.deltaTime;
        transform.position += move;
        if (pS.thunder)
        {
            SpawnThunder();
        }

        animator.SetBool("IsMoving", direction.magnitude > 0);
        DirectionOfMove();
        if (pS.mana <= 0)
            aM.OnActivateMagic();
        if (pS.stamina <= 10)
        {
            pSp.OnUnSprint();
        }
    }

    private void SpawnThunder()
    {
        if (numberOfThunder < 1)
        {
            numberOfThunder++;
            Instantiate(sM.thunder, transform.position, Quaternion.identity);
        }
        else if (numberOfThunder >= 1)
            numberOfThunder--;
        else if (numberOfThunder <= 0)
        {
            numberOfThunder = 0;
        }
    }

    public void DirectionOfMove()
    {
        if (!pS.isPause)
        {
            if (!pS.wind)
            {
                if (direction.x > 0)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    pS.attackTrigger.offset = new Vector2(2.060688f, -1.463209f);
                    pS.attackTrigger.size = new Vector2(2.59382343f, 4.44918299f);
                }
                else if (direction.x < 0)
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    pS.attackTrigger.offset = new Vector2(2.060688f, -1.463209f);
                    pS.attackTrigger.size = new Vector2(2.59382343f, 4.44918299f);
                }
                else if (direction.y > 0)
                {
                    pS.attackTrigger.offset = new Vector2(0.032356739f, 1.15704775f);
                    pS.attackTrigger.size = new Vector2(3.27256441f, 2.77600956f);
                }
                else if (direction.y < 0)
                {
                    pS.attackTrigger.offset = new Vector2(0.032356739f, -4.789396f);
                    pS.attackTrigger.size = new Vector2(3.27256441f, 2.297656f);
                }
                lastOffset = pS.attackTrigger.offset;
                lastSize = pS.attackTrigger.size;
            }
            else
            {
                if (direction.x > 0) transform.rotation = Quaternion.Euler(0, 0, 0);
                else if (direction.x < 0) transform.rotation = Quaternion.Euler(0, 180, 0);
                pS.attackTrigger.offset = new Vector2(0.032356739f, -1.463209f);
                pS.attackTrigger.size = new Vector2(8f, 8f);
            }
        }
    }
}
