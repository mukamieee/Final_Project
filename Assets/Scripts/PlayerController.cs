using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;

    private Rigidbody rb;
    private Vector3 movement;
    private Camera mainCamera;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (mainCamera != null)
        {
            // Get camera directions flattened to ground
            Vector3 camForward = mainCamera.transform.forward;
            Vector3 camRight = mainCamera.transform.right;
            camForward.y = 0f;
            camRight.y = 0f;
            camForward.Normalize();
            camRight.Normalize();

            // Move relative to camera direction
            movement = (camForward * v + camRight * h).normalized;
        }
        else
        {
            // Fallback if no camera found
            movement = new Vector3(h, 0f, v).normalized;
        }
    }

    void FixedUpdate()
    {
        if (movement.magnitude > 0.1f)
        {
            // Move player
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

            // Smoothly rotate to face movement direction
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            rb.rotation = Quaternion.Slerp(
                rb.rotation,
                targetRotation,
                rotationSpeed * Time.fixedDeltaTime);
        }
    }

    // Public so power-up can modify it
    public void SetSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }
}