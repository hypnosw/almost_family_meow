using UnityEngine;

public class FPSPlayerController : MonoBehaviour
{

    public float speed = 5f;
    public float jumpHeight = 0.4f;

    public float gravity = 9.81f;
    private Vector3 input;
    Vector3 moveDirection;
    CharacterController controller;
    public float airControl = 2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();

        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach (Collider col in colliders)
        {
            Debug.Log("Found Collider on: " + col.gameObject.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
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
                moveDirection.y = -0.1f;
            }
        }
        else
        {
            input.y = moveDirection.y;
            moveDirection = Vector3.Lerp(moveDirection, input, airControl * Time.deltaTime);
        }
        moveDirection.y -= gravity * Time.deltaTime;
        Vector3 finalMove = new Vector3(moveDirection.x * speed, moveDirection.y, moveDirection.z * speed);
        controller.Move(finalMove * Time.deltaTime);
    }
}
