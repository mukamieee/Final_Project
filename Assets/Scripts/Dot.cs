using UnityEngine;

public class Dot : MonoBehaviour
{
    [Header("Settings")]
    public int pointValue = 10;
    public float rotateSpeed = 90f;

    void Update()
    {
        // Spin the coin so it looks attractive
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Add points
            if (ScoreManager.Instance != null)
                ScoreManager.Instance.AddPoints(pointValue);

            // Play sound
            if (GameAudioManager.Instance != null)
                GameAudioManager.Instance.PlayDotCollect();

            Debug.Log("Coin collected! +" + pointValue);
            Destroy(gameObject);
        }
    }
}