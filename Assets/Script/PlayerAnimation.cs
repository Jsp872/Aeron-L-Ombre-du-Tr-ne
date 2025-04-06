using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] EnemyManager eM;
    [SerializeField] GameObject player;
    [SerializeField] TriggerManager tM;
    public int whereToLeave;

    [SerializeField] AudioManager aM;
    [SerializeField] AudioSource audioSource;
    public int audioForTheZone;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            eM.RespawnEnemy();
            eM.enemies.Clear();
            NewPositionOfPlayer newPos = collision.GetComponentInParent<NewPositionOfPlayer>();
            player.transform.position = newPos.dungeonPos;
            newPos.oldMap.SetActive(false);
            CancelAnim();
        }
    }

    public void CancelAnim()
    {
        tM.anim = false;
        player.SetActive(true);
        tM.fixedCamera.enabled = true;
        tM.playerInput.enabled = true;
        gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        Leave();
    }

    void Leave()
    {
        if (tM.anim)
        {
            switch (whereToLeave)
            {
                case 1:
                    gameObject.transform.position += new Vector3(0, -0.05f, 0);
                    break;
                case 2:
                    gameObject.transform.position += new Vector3(0, 0.05f, 0);
                    break;
                case 3:
                    gameObject.transform.position += new Vector3(-0.05f, 0, 0);
                    break;
                case 4:
                    gameObject.transform.position += new Vector3(0.05f, 0, 0);
                    break;
            }
            switch (audioForTheZone)
            {
                case 0:
                    audioSource.resource = aM.song[0];
                    break;
                case 1:
                    audioSource.resource = aM.song[1];
                    break;
                case 2:
                    audioSource.resource = aM.song[2];
                    break;
                case 3:
                    audioSource.resource = aM.song[3];
                    break;
                case 4:
                    audioSource.resource = aM.song[4];
                    break;
            }
            audioSource.Play();
        }
    }
}