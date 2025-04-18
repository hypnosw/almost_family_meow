using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class FPSPlayerController : MonoBehaviour
{

    public float speed = 5f;
    private float baseSpeed = 5f;
    public float jumpHeight = 0.1f;

    public float gravity = 9.81f;
    public float airControl = 2;
    public float rotationSpeed = 5f;
    public Image rollerSkatesIcon;
    public Image potionIcon;
    public Material invisibleMat;
    Transform cameraTransform;
    private Vector3 input;
    Vector3 moveDirection;
    CharacterController controller;
    public static bool isRunning { get; private set; }
    public static bool isInvisible { get; private set; }
    private bool isSpeedBoosted = false;
    PlayerStatus playerStatus;
    Material catMat;

    Animator animator;
    int animState;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        baseSpeed = speed;
        playerStatus = GetComponent<PlayerStatus>();
        animator = GetComponentInChildren<Animator>();
        cameraTransform = Camera.main.transform;
        catMat = GetComponentInChildren<SkinnedMeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerStatus.isAlive)
        {
            if (animator != null)
            {
                animator.SetInteger("animState", 3);
            }
            return;
        }

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        input = new Vector3(moveHorizontal, 0, moveVertical);
        input.Normalize();

        if(input.magnitude > 0)
        {
            float rotationAngle = Mathf.Atan2(input.x, input.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            transform.rotation = Quaternion.Euler(0, rotationAngle, 0);

            Vector3 moveDir = Quaternion.Euler(0, rotationAngle, 0) * Vector3.forward;
            input = moveDir.normalized;

            if (!isSpeedBoosted)
            {
                if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && playerStatus.currentEnergy > 0)
                {
                    isRunning = true;
                    animState = 2;
                    speed = baseSpeed * 2;
                }
                else
                {
                    isRunning = false;
                    animState = 1;
                    speed = baseSpeed;
                }
            }
        }
        else
        {
            animState = 0;
        }

        if (animator != null)
        {
            animator.SetInteger("animState", animState);
        }

        if ((Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)) && rollerSkatesIcon.enabled == true && !isSpeedBoosted)
        {
            StartCoroutine(SpeedBoost());
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && potionIcon.enabled == true)
        {
            StartCoroutine(ActivateInvisibility());
        }

        if (controller.isGrounded)
        {
            moveDirection = input;
            moveDirection.y = 0.0f;
        }
        else
        {
            input.y = moveDirection.y;
            moveDirection = Vector3.Lerp(moveDirection, input, airControl * Time.deltaTime);
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * speed * Time.deltaTime);
    }

    IEnumerator SpeedBoost()
    {
        isSpeedBoosted = true;
        speed = baseSpeed * 3;
        rollerSkatesIcon.enabled = false;
        PlayerPrefs.SetInt("Powerup_collected_1", 0);
        PlayerPrefs.Save();
        yield return new WaitForSeconds(5);
        speed = baseSpeed;
        isSpeedBoosted = false;
    }

    IEnumerator ActivateInvisibility()
    {
        isInvisible = true;
        potionIcon.enabled = false;
        PlayerPrefs.SetInt("Powerup_collected_2", 0);
        PlayerPrefs.Save();
        SkinnedMeshRenderer renderer = GetComponentInChildren<SkinnedMeshRenderer>();
        
        if (renderer != null)
        {
            renderer.material = invisibleMat;
            yield return new WaitForSeconds(5);
            renderer.material = catMat;
            isInvisible = false;
        }
    }
}
