using UnityEngine;
using System.Collections;

public class PowerUpSpeed : MonoBehaviour
{
    [Header("Settings")]
    public float speedBoostAmount = 3f;
    public float boostDuration = 5f;
    public float rotateSpeed = 90f;

    void Update()
    {
        // Spin so it looks attractive
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
                StartCoroutine(ApplySpeedBoost(player));

            if (GameAudioManager.Instance != null)
                GameAudioManager.Instance.PlaySpeedBoost();

            Debug.Log("Speed Boost activated!");
            Destroy(gameObject);
        }
    }

    IEnumerator ApplySpeedBoost(PlayerController player)
    {
        float originalSpeed = player.moveSpeed;
        player.moveSpeed += speedBoostAmount;

        Debug.Log($"Speed increased to {player.moveSpeed}");

        yield return new WaitForSeconds(boostDuration);

        player.moveSpeed = originalSpeed;
        Debug.Log("Speed boost ended!");
    }
}