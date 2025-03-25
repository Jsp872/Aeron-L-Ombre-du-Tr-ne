using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationLeave : MonoBehaviour
{

    [SerializeField] GameObject player;
    [SerializeField] GameObject playerAnimation;

    bool anim;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            playerAnimation.SetActive(true);
            playerAnimation.transform.position = player.transform.position;
            player.SetActive(false);
            anim = true;
        }
        if (collision.gameObject.layer == 11)
        {
            SceneManager.LoadScene("Plain");
        }
    }

    private void FixedUpdate()
    {
        if (anim)
        {
            playerAnimation.transform.position += new Vector3(0, -0.05f, 0);
        }
    }
}
