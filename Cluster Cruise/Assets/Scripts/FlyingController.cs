using UnityEngine;

// Basic flying player controller
public class FlyingController : MonoBehaviour
{
    public float speed = 10f;
    public float mouseSensitivity = 2f;
    public float upDownRange = 60f;

    private float rotationX = 0f;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Handle mouse look
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -upDownRange, upDownRange);

        transform.localRotation = Quaternion.Euler(rotationX, transform.localEulerAngles.y + mouseX, 0);

        // Handle movement
        if (Input.GetKey(KeyCode.Space))
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }
}
