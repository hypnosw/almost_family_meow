using Unity.VisualScripting;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    Transform player;
    public float speed = 5f;
    public float maxDistance = 15f;
    PlayerStatus playerStatus;
    FPSPlayerController playerController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerStatus = player.GetComponent<PlayerStatus>();
        playerController = player.GetComponent<FPSPlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            return;
        }

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > maxDistance)
        {
            Destroy(gameObject);
            return;
        }
        float step = speed * Time.deltaTime;
        transform.LookAt(player);
        if (playerController.isInvisible == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, step);
        }
    }

    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            playerStatus.Die();
            Destroy(gameObject);
        }
    }
}
