using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector3 direction;
    public float moveSpeed = 5f;
    [SerializeField] private InputActionReference inputActionMove;
    private Animator animator;
    [SerializeField] private BoxCollider boxCollider;

    [SerializeField] int life= 3;

    [SerializeField] GameObject UIDefeat;
    public Rigidbody rb;

    bool attack;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        direction = inputActionMove.action.ReadValue<Vector3>().normalized;
        Vector3 move = new Vector3(direction.x, 0, direction.z) * moveSpeed * Time.deltaTime;
        transform.position += move;

        animator.SetBool("IsMoving", direction.magnitude > 0);

        DirectionOfMove();
    }

    private void DirectionOfMove()
    {
        if (direction.x > 0)
            transform.rotation = Quaternion.Euler(0, 90, 0);
        else if (direction.x < 0)
            transform.rotation = Quaternion.Euler(0, -90, 0);
        else if (direction.z > 0 && direction.x == 0)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        else if (direction.z < 0 && direction.x == 0)
            transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    public void OnAttack()
    {
        animator.SetTrigger("Attack");
        boxCollider.enabled = true;
        if (!attack)
            StartCoroutine(TimeOfAttack());
    }

    IEnumerator TimeOfAttack()
    {
        attack = true;
        yield return new WaitForSeconds(0.75f);
        boxCollider.enabled = false;
        attack = false;
    }


    public void TakeDamage(int damage)
    {
        life -= damage;
        if (life <= 0)
        {
            Destroy(gameObject);
            Time.timeScale = 0;
            UIDefeat.SetActive(true);
        }
    }
}
