using System.Collections;
using UnityEngine;

public class SelectMagic : MonoBehaviour
{
    public GameObject UiMenuMagic;
    public GameObject fireBall;
    public GameObject thunder;

    [HideInInspector] public int magicNumber;
    [HideInInspector] public bool activateMagic;

    private ActivateMagic aM;
    private PlayerStat pS;
    private void Awake()
    {
        pS = GetComponent<PlayerStat>();
        aM = GetComponent<ActivateMagic>();
    }

    public IEnumerator OnMagicMenu()
    {
        UiMenuMagic.SetActive(true);

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (activateMagic)
            {
                UiMenuMagic.SetActive(false);
                activateMagic = false;
            }
            else 
                activateMagic = true;
        }

        while (UiMenuMagic.activeSelf)
        {
            if (activateMagic)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    magicNumber = 1;
                    UiMenuMagic.SetActive(false);
                    pS.activateMagic = false;
                    aM.OnActivateMagic();
                }
                else if (Input.GetKeyDown(KeyCode.A))
                {
                    magicNumber = 2;
                    UiMenuMagic.SetActive(false);
                    pS.activateMagic = false;
                    aM.OnActivateMagic();
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    magicNumber = 3;
                    UiMenuMagic.SetActive(false);
                    pS.activateMagic = false;
                    aM.OnActivateMagic();
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    magicNumber = 4;
                    UiMenuMagic.SetActive(false);
                    pS.activateMagic = false;
                    aM.OnActivateMagic();
                }
            }
            yield return null;
        }
    }
}