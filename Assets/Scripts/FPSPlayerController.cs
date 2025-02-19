using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class FPSPlayerController : MonoBehaviour
{

    public float speed = 5f;
    private float baseSpeed = 5f;
    public float jumpHeight = 0.1f;

    public float gravity = 9.81f;
    public Image rollerSkatesIcon;
    public Image potionIcon;
    private Vector3 input;
    Vector3 moveDirection;
    CharacterController controller;
    public float airControl = 2;
    public bool isRunning { get; private set; }
    private bool isSpeedBoosted = false;
    PlayerStatus playerStatus;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        baseSpeed = speed;
        playerStatus = GetComponent<PlayerStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerStatus.isAlive)
        {
            return;
        }
        if (!isSpeedBoosted)
        {
            if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && playerStatus.currentEnergy > 0)
            {
                isRunning = true;
                speed = baseSpeed * 2;
            }
            else
            {
                isRunning = false;
                speed = baseSpeed;
            }
        }
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        input = transform.right * moveHorizontal + transform.forward * moveVertical;
        input.Normalize();

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
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = Mathf.Sqrt(2 * jumpHeight * gravity);

            }
            else
            {
                moveDirection.y = 0.0f;
            }
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
        yield return new WaitForSeconds(5);
        speed = baseSpeed;
        isSpeedBoosted = false;
    }

    IEnumerator ActivateInvisibility()
    {
        SkinnedMeshRenderer renderer = GetComponentInChildren<SkinnedMeshRenderer>(true);
        
        if (renderer != null)
        {
            Material playerMaterial = renderer.material;

            playerMaterial.SetFloat("_SurfaceType", 1);
            playerMaterial.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;

            potionIcon.enabled = false;
            Debug.Log("Player Surface Type: Transparent");

            yield return new WaitForSeconds(5);

            playerMaterial.SetFloat("_SurfaceType", 0);
            playerMaterial.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Geometry;

            Debug.Log("Player Surface Type: Opaque");
        }
        else
        {
            Debug.LogWarning("SkinnedMeshRenderer not found on player!");
        }
    }
}
