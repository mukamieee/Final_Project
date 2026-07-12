using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("Camera Settings")]
    public float distance = 6f;
    public float height = 3f;
    public float smoothSpeed = 8f;

    [Header("Collision")]
    public LayerMask collisionMask;
    public float collisionRadius = 0.3f;

    private Vector3 currentVelocity;

    void LateUpdate()
    {
        if (target == null) return;

        // Position behind and above player
        Vector3 desiredPosition = target.position
            - target.forward * distance
            + Vector3.up * height;

        // Check for wall collision so camera
        // doesn't clip through walls
        RaycastHit hit;
        Vector3 directionToCamera = desiredPosition - target.position;
        float distanceToCamera = directionToCamera.magnitude;

        if (Physics.SphereCast(
            target.position,
            collisionRadius,
            directionToCamera.normalized,
            out hit,
            distanceToCamera,
            collisionMask))
        {
            // Move camera in front of wall
            desiredPosition = hit.point +
                hit.normal * collisionRadius;
        }

        // Smooth camera movement
        transform.position = Vector3.SmoothDamp(
            transform.position,
            desiredPosition,
            ref currentVelocity,
            1f / smoothSpeed);

        // Always look slightly above player
        // so we can see their face
        transform.LookAt(target.position + Vector3.up * 1f);
    }
}