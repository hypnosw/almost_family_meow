using Unity.VisualScripting;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    Transform player;
    public float speed = 5f;
    public float maxDistance = 15f;
    public float lifeTime = 20f;
    PlayerStatus playerStatus;
    private AudioSource audioSource;
    Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerStatus = player.GetComponent<PlayerStatus>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponentInChildren<Animator>();
        if (audioSource != null)
        {
            audioSource.Play();
        }
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            return;
        }

        // float distance = Vector3.Distance(transform.position, player.position);

        // if (distance > maxDistance)
        // {
        //     Destroy(gameObject);
        //     return;
        // }
        float step = speed * Time.deltaTime;
        if (FPSPlayerController.isInvisible == false)
        {
            transform.LookAt(player);
            transform.position = Vector3.MoveTowards(transform.position, player.position, step);
            animator.SetBool("stopChasing", false);
        } else {
            animator.SetBool("stopChasing", true);
        }
    }

    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            playerStatus.Die();
            Destroy(gameObject);
        }
    }
}
