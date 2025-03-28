using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    Transform target;
    [SerializeField] Transform player;
    public bool touch;

    bool oneTimeSaveScale = false;
    float scaleSave;

    private void Awake()
    {
        target = GetComponent<Transform>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            Transform enemyTransform = collision.transform.parent;
            Animator enemyAnimator = collision.GetComponentInParent<Animator>();
            Enemy enemy = collision.GetComponentInParent<Enemy>();

            Vector2 direction = (target.position - enemyTransform.position).normalized;

            if (!oneTimeSaveScale)
            {
                if (enemyTransform.localScale.x > 0)
                    scaleSave = enemyTransform.localScale.x;
                else if (enemyTransform.localScale.x < 0)
                    scaleSave = -enemyTransform.localScale.x;
                oneTimeSaveScale = true;
            }
            if (direction.x >= 0)
                enemyTransform.localScale = new Vector3(scaleSave, enemyTransform.localScale.y, enemyTransform.localScale.z);
            else if (direction.x < 0)
                enemyTransform.localScale = new Vector3(-scaleSave, enemyTransform.localScale.y, enemyTransform.localScale.z);
               

            if (!touch)
            {
                enemyTransform.position = Vector2.MoveTowards(enemyTransform.position, target.position, enemy.moveSpeed * Time.deltaTime);
            }
        }
        if (collision.gameObject.layer == 10)
        {
            player.transform.position = new Vector2(0, 15);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        oneTimeSaveScale = false;
    }
}
