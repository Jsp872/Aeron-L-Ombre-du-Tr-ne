using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.InputSystem.EnhancedTouch;

public class TriggerManager : MonoBehaviour
{
    Transform target;

    [SerializeField] float speed = 2f;
    [SerializeField] float rotationSpeed = 2f;
    public bool touch;

    private void Awake()
    {
        target = GetComponent<Transform>();
    }
    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.layer == 9)
        {

            Transform enemyTransform = collision.transform.parent;
            Animator enemyAnimator = collision.GetComponentInParent<Animator>();

            Vector3 direction = (target.position - enemyTransform.position).normalized;
            direction.y = 0;

            Quaternion targetRotation = Quaternion.LookRotation(-direction);
            enemyTransform.rotation = Quaternion.Slerp(enemyTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);


            if (!touch)
            {
                enemyTransform.position = Vector3.MoveTowards(enemyTransform.position, target.position, speed * Time.deltaTime);
                enemyAnimator.SetBool("IsMoving", true);
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        Animator enemyAnimator = other.GetComponentInParent<Animator>();
        enemyAnimator.SetBool("IsMoving", false);
    }
}
