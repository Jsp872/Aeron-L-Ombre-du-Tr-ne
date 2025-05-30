using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    [Header("Enemy Stats")]
    public int moveSpeed;
    public int damage;
    public int propulsionForce;
    public float maxLife;
    public int valueOfEnemy;

    [Header("Enemy Behaviour")]
    public bool bossCheck;
    public bool inverseLook;
    public bool haveAnim;
    public bool differentSprite;

    [HideInInspector] public float life;
    [HideInInspector] public Vector2 initialPosition;
    public Vector2 lastDirection = Vector2.down;

    private void Awake()
    {
        initialPosition = transform.position;
        life = maxLife;
    }

    private void OnDisable()
    {
        if (!bossCheck)
        {
            transform.position = initialPosition;
        }
    }
}