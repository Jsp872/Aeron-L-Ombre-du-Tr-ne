using UnityEngine;

public class SetMagic : MonoBehaviour
{
    private AttackTrigger aT;
    private PlayerMovement pM;
    private ActivateMagic aM;
    private PlayerStat pS;
    private void Awake()
    {
        aT = GetComponentInChildren<AttackTrigger>();
        aM = GetComponent<ActivateMagic>();
        pM = GetComponent<PlayerMovement>();
        pS = GetComponent<PlayerStat>();
    }

    public void SetFire()
    {
        pS.damage = pS.stockDamage * 2;
        if (pS.isRunning)
            pS.moveSpeed = 10;
        else pS.moveSpeed = 5;
        pS.moveSpeed = 5;
        pS.attackTrigger.offset = pM.lastOffset;
        pS.attackTrigger.size = pM.lastSize;
        pS.ice = false;
        pS.wind = false;
        pS.fire = true;
        pS.thunder = false;
        pS.propulsionForce = 25;
        pS.activateMagic = true;
    }

    public void SetIce()
    {
        pS.damage = pS.stockDamage;
        if (pS.isRunning)
            pS.moveSpeed = 10;
        else pS.moveSpeed = 5;
        pS.attackTrigger.offset = pM.lastOffset;
        pS.attackTrigger.size = pM.lastSize;
        pS.ice = true;
        pS.wind = false;
        pS.fire = false;
        pS.thunder = false;
        pS.propulsionForce = 25;
        pS.activateMagic = true;
    }

    public void SetThunder()
    {
        pS.damage = pS.stockDamage;
        if (pS.isRunning)
            pS.moveSpeed = 16;
        else pS.moveSpeed = 8;
        pS.attackTrigger.offset = pM.lastOffset;
        pS.attackTrigger.size = pM.lastSize;
        pS.ice = false;
        pS.wind = false;
        pS.fire = false;
        pS.thunder = true;
        pS.propulsionForce = 25;
        pS.activateMagic = true;
    }

    public void SetWind()
    {
        pS.damage = pS.stockDamage;
        if (pS.isRunning)
            pS.moveSpeed = 10;
        else pS.moveSpeed = 5;
        pS.moveSpeed = 5;
        pS.ice = false;
        pS.wind = true;
        pS.fire = false;
        pS.thunder = false;
        pS.propulsionForce = 50;
        pS.activateMagic = true;
    }

    public void SetNormal()
    {
        pS.damage = pS.stockDamage;
        pS.moveSpeed = 5;
        pS.attackTrigger.offset = pM.lastOffset;
        pS.attackTrigger.size = pM.lastSize;
        pS.ice = false;
        pS.wind = false;
        pS.fire = false;
        pS.thunder = false;
        pS.propulsionForce = 25;
    }
}