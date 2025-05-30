using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ActivateMagic : MonoBehaviour
{
    public static event Action<float> OnManaChanged;
    [SerializeField] ParticleSystem particleMagic;
    SetMagic sT;
    SelectMagic sM;
    private PlayerStat pS;

    [SerializeField] BossKilledManager bKM;

    [SerializeField] Image UpCircleMagic;

    private void Awake()
    {
        sT = GetComponent<SetMagic>();
        sM = GetComponent<SelectMagic>();
        pS = GetComponent<PlayerStat>();
    }
    public void OnActivateMagic()
    {
        sM.activateMagic = false;
        if (!sM.UiMenuMagic.activeSelf)
        {
            var mainModule = particleMagic.main;

            if (pS.activateMagic)
            {
                UpCircleMagic.color = Color.black;
                particleMagic.Stop();
                pS.activateMagic = false;
                sT.SetNormal();
            }
            else if (pS.mana >= 10)
            {
                switch (sM.magicNumber)
                {
                    case 1:
                        if (bKM.numberOfBossKilled >= 2)
                        {
                            UpCircleMagic.color = Color.red;
                            mainModule.startColor = Color.red;
                            sT.SetNormal();
                            sT.SetFire();
                        }
                        break;

                    case 2:
                        if (bKM.numberOfBossKilled >= 3)
                        {
                            UpCircleMagic.color = Color.cyan;
                            mainModule.startColor = Color.cyan;
                            sT.SetNormal();
                            sT.SetIce();
                        }
                        break;

                    case 3:
                        if (bKM.numberOfBossKilled >= 4)
                        {
                            UpCircleMagic.color = Color.yellow;
                            mainModule.startColor = Color.yellow;
                            sT.SetNormal();
                            sT.SetThunder();
                        }
                        break;

                    case 4:
                        if (bKM.numberOfBossKilled >= 1)
                        {
                            UpCircleMagic.color = Color.white;
                            mainModule.startColor = Color.white;
                            sT.SetNormal();
                            sT.SetWind();
                        }
                        break;

                    default:
                        return;
                }
                if (pS.activateMagic)
                {
                    particleMagic.Play();
                    StartCoroutine(drainMana());
                }
                else
                {
                    UpCircleMagic.color = Color.black;
                    particleMagic.Stop();
                    sT.SetNormal();
                }
            }
        }
    }
    IEnumerator drainMana()
    {
        while (pS.activateMagic)
        {
            pS.mana = Mathf.Max(pS.mana - 10, 0);
            OnManaChanged?.Invoke(pS.mana);
            yield return new WaitForSeconds(1);
        }
    }
}