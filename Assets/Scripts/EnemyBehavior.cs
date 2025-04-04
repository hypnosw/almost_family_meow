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
    LevelManager levelManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (player != null)
        {
            playerStatus = player.GetComponent<PlayerStatus>();
        }
        audioSource = GetComponent<AudioSource>();
        animator = GetComponentInChildren<Animator>();
        if (audioSource != null)
        {
            audioSource.Play();
        }
        Destroy(gameObject, lifeTime);
        levelManager = FindAnyObjectByType<LevelManager>();
        levelManager.DisplayTutorialMessage("A dog is chasing you!\nPress SHIFT to run \nOr find a crate to hide in!");
    }

    // Update is called once per frame
    void Update()
    {
        if(LevelManager.isPlaying == false)
        {
            Destroy(gameObject);
            return;
        }
        if (player != null)
        {
            float step = speed * Time.deltaTime;
            if (FPSPlayerController.isInvisible == false && HideoutBehavior.isHidden == false)
            {
                transform.LookAt(player);
                transform.position = Vector3.MoveTowards(transform.position, player.position, step);
                animator.SetBool("stopChasing", false);
            }
            else {
                animator.SetBool("stopChasing", true);
            }
        } else
        {
            Debug.LogWarning("Player not found. Destroying enemy.");
            Destroy(gameObject);
            return;
        }

        // float distance = Vector3.Distance(transform.position, player.position);

        // if (distance > maxDistance)
        // {
        //     Destroy(gameObject);
        //     return;
        // }
    }

    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            playerStatus.Die();
            Destroy(gameObject);
        }
    }
}
