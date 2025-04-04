using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

public class PlayerMagic : MonoBehaviour
{
    public static event Action<float> OnManaChanged;

    bool upMagic;
    [SerializeField] Image UpCircleMagic;
    [SerializeField] GameObject UiMenuMagic;
    [SerializeField] TileMapManager tMM;

    ParticleSystem particleMagic;

    int magicNumber;
    public bool activate;

    AttackTrigger aT;
    PlayerController player;

    private void Awake()
    {
        particleMagic = GetComponent<ParticleSystem>();
        aT = GetComponentInChildren<AttackTrigger>();
        player = GetComponent<PlayerController>();
    }

    IEnumerator drainMana()
    {
        while (activate)
        {
            player.mana = Mathf.Max(player.mana - 10, 0);
            OnManaChanged?.Invoke(player.mana);
            yield return new WaitForSeconds(1);
        }
    }

    public IEnumerator OnMagicMenu()
    {
        UiMenuMagic.SetActive(true);

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (upMagic)
            {
                UiMenuMagic.SetActive(false);
                upMagic = false;
            }
            else 
                upMagic = true;
        }
        //else if (Input.GetKeyDown(KeyCode.F))
        //{
        //    upMagic = false;
        //}

        while (UiMenuMagic.activeSelf)
        {
            if (upMagic)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    magicNumber = 1;
                    UiMenuMagic.SetActive(false);
                    activate = false;
                    OnActivateMagic();
                }
                else if (Input.GetKeyDown(KeyCode.A))
                {
                    magicNumber = 2;
                    UiMenuMagic.SetActive(false);
                    activate = false;
                    OnActivateMagic();
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    magicNumber = 3;
                    UiMenuMagic.SetActive(false);
                    activate = false;
                    OnActivateMagic();
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    magicNumber = 4;
                    UiMenuMagic.SetActive(false);
                    activate = false;
                    OnActivateMagic();
                }
            }

            yield return null;
        }
    }

    void SetFire()
    {
        aT.damage = PlayerPrefs.GetFloat("damage", 1) * 2;
        if (player.isRunning)
            player.moveSpeed = 10;
        else player.moveSpeed = 5;
        player.moveSpeed = 5;
        player.ice = false;
        aT.propulsionForce = 25;
        activate = true;
    }

    void SetIce()
    {
        aT.damage = PlayerPrefs.GetFloat("damage", 1);
        if (player.isRunning)
            player.moveSpeed = 10;
        else player.moveSpeed = 5;
        player.ice = true;
        aT.propulsionForce = 25;
        activate = true;
    }

    void SetThunder()
    {
        aT.damage = PlayerPrefs.GetFloat("damage", 1);
        if (player.isRunning)
          player.moveSpeed = 16;
        else player.moveSpeed = 8;
        player.ice = false;
        aT.propulsionForce = 25;
        activate = true;
    }

    void SetWind()
    {
        aT.damage = PlayerPrefs.GetFloat("damage", 1);
        if (player.isRunning)
            player.moveSpeed = 10;
        else player.moveSpeed = 5;
        player.moveSpeed = 5;
        player.ice = false;
        aT.propulsionForce = 50;
        activate = true;
    }

    void SetNormal()
    {
        aT.damage = PlayerPrefs.GetFloat("damage", 1);
        player.moveSpeed = 5;
        player.ice = false;
        aT.propulsionForce = 25;
    }

    public void OnActivateMagic()
    {
        upMagic = false;
        if (!UiMenuMagic.activeSelf)
        {
            var mainModule = particleMagic.main;

            if (activate)
            {
                UpCircleMagic.color = Color.black;
                particleMagic.Stop();
                activate = false;
                SetNormal();
            }
            else if (player.mana >= 10)
            {
                switch (magicNumber)
                {
                    case 1:
                        if (tMM.numberOfBossKilled >= 2)
                        {
                            UpCircleMagic.color = Color.red;
                            mainModule.startColor = Color.red;
                            SetFire();
                        }
                        break;

                    case 2:
                        if (tMM.numberOfBossKilled >= 3)
                        {
                            UpCircleMagic.color = Color.cyan;
                            mainModule.startColor = Color.cyan;
                            SetIce();
                        }
                        break;

                    case 3:
                        if (tMM.numberOfBossKilled >= 4)
                        {
                            UpCircleMagic.color = Color.yellow;
                            mainModule.startColor = Color.yellow;
                            SetThunder();
                        }
                        break;

                    case 4:
                        if (tMM.numberOfBossKilled >= 1)
                        {
                            UpCircleMagic.color = Color.white;
                            mainModule.startColor = Color.white;
                            SetWind();
                        }
                        break;

                    default:
                        return;
                }
                if (activate)
                {
                    particleMagic.Play();
                    StartCoroutine(drainMana());
                }
                else
                {
                    UpCircleMagic.color = Color.black;
                    particleMagic.Stop();
                    SetNormal();
                }
            }
        }
    }
}
