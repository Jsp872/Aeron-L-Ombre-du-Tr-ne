using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;
    private SelectMagic sM;
    private PlayerMovement pM;
    private PlayerStat pS;

    private Quaternion rotation;
    private Quaternion lastRotation;

    [SerializeField] private ParticleSystem particleForWind;

    private void Awake()
    {
        pS = GetComponent<PlayerStat>();
        sM = GetComponent<SelectMagic>();
        pM = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
    }
    public void OnAttack()
    {
        if (pS.stamina >= 20 && !pS.attack && !pS.isPause)
        {
            pS.stamina -= 20;
            pS.OnStaminaChange(pS.stamina);
            animator.SetTrigger("Attack");
            if (pS.fire)
            {
                LunchFireBall();
            }
            else if (pS.wind)
                particleForWind.Play();
            StartCoroutine(TimeOfAttack());
        }
    }

    private void LunchFireBall()
    {
        if (sM.fireBall != null)
        {
            if (pM.direction.x > 0) rotation = Quaternion.Euler(0, 0, 0);
            else if (pM.direction.x < 0) rotation = Quaternion.Euler(0, 0, 180);
            else if (pM.direction.y > 0) rotation = Quaternion.Euler(0, 0, 90);
            else if (pM.direction.y < 0) rotation = Quaternion.Euler(0, 0, -90);
            else rotation = lastRotation;
            lastRotation = rotation;
            Instantiate(sM.fireBall, transform.position, rotation);
        }
    }

    public IEnumerator TimeOfAttack()
    {
        pS.attack = true;
        yield return new WaitForSeconds(0.75f);
        pS.attack = false;
    }
}