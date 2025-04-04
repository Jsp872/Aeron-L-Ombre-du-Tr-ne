using TMPro;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI lifeText;
    [SerializeField] TextMeshProUGUI staminaText;
    [SerializeField] TextMeshProUGUI manaText;
    [SerializeField] TextMeshProUGUI attackText;
    [SerializeField] TextMeshProUGUI moveSpeedText;

    [SerializeField] GameObject Stats;
    bool isStats;
    PlayerController pC;
    AttackTrigger aT;

    private void Awake()
    {
        pC = GetComponent<PlayerController>();
        aT = GetComponentInChildren<AttackTrigger>();
    }

    private void FixedUpdate()
    {
        if (isStats)
        {
            lifeText.text = "Vie: " + pC.life;
            staminaText.text = "Endurance: " + pC.stamina;
            manaText.text = "Mana: " + pC.mana;
            attackText.text = "Attaque: " + aT.damage;
            moveSpeedText.text = "Vitesse de déplacement: " + pC.moveSpeed;
        }
    }

    public void OnCheckStat()
    {
        if (isStats)
        {
            isStats = false;
            Stats.SetActive(false);
        }
        else
        {
            Stats.SetActive(true);
            isStats = true;
        }
    }
}
