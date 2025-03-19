using System.Collections;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    [SerializeField] GameObject UIVictory;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            Animator enemyAnimator = other.GetComponent<Animator>();
            enemyAnimator.SetTrigger("Die");
            StartCoroutine(Die(other.gameObject));
        }

    }
    private IEnumerator Die(GameObject enemy)
    {
        yield return new WaitForSeconds(1f);
        Destroy(enemy);
        UIVictory.SetActive(true);
    }
}
