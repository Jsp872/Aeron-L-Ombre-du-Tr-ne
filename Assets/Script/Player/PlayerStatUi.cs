using TMPro;
using UnityEngine;

public class PlayerStatUi : MonoBehaviour
{
    [Header("StatPlayerText")]
    [SerializeField] private TextMeshProUGUI lifeText;
    [SerializeField] private TextMeshProUGUI staminaText;
    [SerializeField] private TextMeshProUGUI manaText;
    [SerializeField] private TextMeshProUGUI attackText;
    [SerializeField] private TextMeshProUGUI moveSpeedText;

    [Header("StatsGameObject")]
    [SerializeField] private GameObject Stats;

    private bool isStats;
    private PlayerStat pS;

    private void Awake()
    {
        pS = GetComponent<PlayerStat>();
    }

    private void FixedUpdate()
    {
        if (isStats)
        {
            lifeText.text = "Vie: " + pS.life;
            staminaText.text = "Endurance: " + pS.stamina;
            manaText.text = "Mana: " + pS.mana;
            attackText.text = "Attaque: " + pS.damage;
            moveSpeedText.text = "Vitesse de déplacement:   " + pS.moveSpeed;
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