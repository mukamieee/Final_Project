using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Shooting")]
    public GameObject projectilePrefab;
    public Transform shootPoint;
    public float projectileSpeed = 15f;
    public float shootCooldown = 0.5f;

    [Header("State")]
    public bool canShoot = false;

    private float lastShootTime = 0f;

    void Start()
    {
        // Auto create shoot point if not assigned
        if (shootPoint == null)
        {
            GameObject sp = new GameObject("ShootPoint");
            sp.transform.SetParent(transform);
            sp.transform.localPosition = new Vector3(0f, 0f, 1f);
            shootPoint = sp.transform;
        }
    }

    void Update()
    {
        if (!canShoot) return;
        if (GameManager.Instance != null && GameManager.Instance.isGameOver) return;

        if (Input.GetKeyDown(KeyCode.Space) &&
            Time.time >= lastShootTime + shootCooldown)
        {
            Shoot();
        }
    }

    public void EnableShooting()
    {
        canShoot = true;
        Debug.Log("Shooting enabled! Press Space to shoot.");
    }

    void Shoot()
    {
        if (projectilePrefab == null)
        {
            Debug.LogWarning("No projectile prefab assigned!");
            return;
        }

        lastShootTime = Time.time;

        // Spawn projectile at shoot point
        GameObject proj = Instantiate(
            projectilePrefab,
            shootPoint.position,
            shootPoint.rotation);

        Rigidbody rb = proj.GetComponent<Rigidbody>();
        if (rb != null)
            rb.linearVelocity = shootPoint.forward * projectileSpeed;

        if (GameAudioManager.Instance != null)
            GameAudioManager.Instance.PlayProjectileFire();

        // Destroy projectile after 3 seconds
        Destroy(proj, 3f);

        Debug.Log("Shot fired!");
    }
}