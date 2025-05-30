using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [HideInInspector] public int whereToLeave;
    [HideInInspector] public int audioForTheZone;

    [SerializeField] private float speed;

    [Header("Reference")]
    [SerializeField] private GameObject player;
    [SerializeField] private EnemyManager eM;
    [SerializeField] private AudioSource audioSource;

    private TpPlayerTrigger tPT;
    private AudioManager aM;
    private Vector3 directionToGo;

    private void Awake()
    {
        tPT = player.GetComponentInChildren<TpPlayerTrigger>();
        aM = audioSource.GetComponent<AudioManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            eM.RespawnEnemy();
            eM.enemies.Clear();
            NewPositionOfPlayer newPos = collision.GetComponentInParent<NewPositionOfPlayer>();
            player.transform.position = newPos.dungeonPos;
            newPos.oldMap.SetActive(false);
            if (newPos.activateBoss)
            {
                if (newPos.boss != null)
                {
                    newPos.boss.SetActive(true);
                }
            }
            else 
                if (newPos.boss != null)
                {
                  newPos.boss.SetActive(false);
                }
            CancelAnim();
        }
    }

    public void CancelAnim()
    {
        tPT.anim = false;
        player.SetActive(true);
        tPT.fixedCamera.enabled = true;
        tPT.playerInput.enabled = true;
        gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        Leave();
    }

    void Leave()
    {
        if (tPT.anim)
        {
            switch (whereToLeave)
            {
                case 1:
                    directionToGo = new Vector3(0, -speed, 0);
                    gameObject.transform.position += directionToGo;
                    break;
                case 2:
                    directionToGo = new Vector3(0, speed, 0);
                    gameObject.transform.position += directionToGo;
                    break;
                case 3:
                    directionToGo = new Vector3(-speed, 0, 0);
                    gameObject.transform.position += directionToGo;
                    break;
                case 4:
                    directionToGo = new Vector3(speed, 0, 0);
                    gameObject.transform.position += directionToGo;
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