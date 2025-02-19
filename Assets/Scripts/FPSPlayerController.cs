using UnityEngine;

public class FPSPlayerController : MonoBehaviour
{

    public float speed = 5f;
    private float baseSpeed = 5f;
    public float jumpHeight = 0.1f;

    public float gravity = 9.81f;
    private Vector3 input;
    Vector3 moveDirection;
    CharacterController controller;
    public float airControl = 2;
    public bool isRunning { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        baseSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)){
            isRunning = true;
            speed = baseSpeed * 2;
        } else {
            isRunning = false;
            speed = baseSpeed;
        }
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        input = transform.right * moveHorizontal + transform.forward * moveVertical;
        input.Normalize();


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
}
