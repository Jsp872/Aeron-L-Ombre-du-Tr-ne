using UnityEngine;

public class PlayerBlock : MonoBehaviour
{
    private PlayerStat pS;

    private void Awake()
    {
        pS = GetComponent<PlayerStat>();
    }
    public void OnBlock()
    {
        if (!pS.isPause)
        {
            pS.moveSpeed = pS.stockMoveSpeed / 2;

            pS.isBlocking = true;
            pS.animator.SetBool("Block", true);
        }
    }

    public void OnUnBlock()
    {
        pS.moveSpeed = pS.stockMoveSpeed;
        pS.isBlocking = false;
        pS.animator.SetBool("Block", false);
    }
}
