using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    Transform playerBody;
    private float pitch;
    public float pitchMin = -20f;
    public float pitchMax = 20f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerBody = transform.parent.transform;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerStatus.isAlive)
        {
            float moveX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float moveY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;


            if (playerBody)
            {
                playerBody.Rotate(Vector3.up * moveX);
            }

            pitch -= moveY;
            pitch = Mathf.Clamp(pitch, pitchMin, pitchMax);
            transform.localRotation = Quaternion.Euler(pitch, 0, 0);
        } else {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

    }
}
