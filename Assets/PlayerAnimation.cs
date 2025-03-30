using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] TriggerManager tM;
    public int whereToLeave;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            NewPositionOfPlayer newPos = collision.GetComponentInParent<NewPositionOfPlayer>();
            player.transform.position = newPos.dungeonPos;
            newPos.oldMap.SetActive(false);
            CancelAnim();
        }
    }

    public void CancelAnim()
    {
        tM.anim = false;
        player.SetActive(true);
        tM.fixedCamera.enabled = true;
        tM.playerInput.enabled = true;
        gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        Leave();
    }

    void Leave()
    {
        if (tM.anim)
        {
            switch (whereToLeave)
            {
                case 1:
                    gameObject.transform.position += new Vector3(0, -0.05f, 0);
                    break;
                case 2:
                    gameObject.transform.position += new Vector3(0, 0.05f, 0);
                    break;
                case 3:
                    gameObject.transform.position += new Vector3(-0.05f, 0, 0);
                    break;
                case 4:
                    gameObject.transform.position += new Vector3(0.05f, 0, 0);
                    break;
            }

        }
    }
}