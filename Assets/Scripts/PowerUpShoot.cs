using UnityEngine;

public class PowerUpShoot : MonoBehaviour
{
    public float rotateSpeed = 90f;

    void Update()
    {
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerShooting shooting = other.GetComponent<PlayerShooting>();
            if (shooting != null)
                shooting.EnableShooting();

            if (GameAudioManager.Instance != null)
                GameAudioManager.Instance.PlayShootPickup();

            Debug.Log("Shooting ability unlocked!");
            Destroy(gameObject);
        }
    }
}