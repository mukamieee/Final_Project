using UnityEngine;

public class ExitPortal : MonoBehaviour
{
    [Header("Settings")]
    public bool requireAllDots = true;
    public float pulseSpeed = 3f;
    public float pulseAmount = 0.15f;

    [Header("Visual")]
    public Light portalLight;

    private Vector3 originalScale;
    private bool playerCanExit = false;

    void Start()
    {
        originalScale = transform.localScale;

        // Auto find light if not assigned
        if (portalLight == null)
            portalLight = GetComponentInChildren<Light>();
    }

    void Update()
    {
        // Pulse scale
        float pulse = 1f + Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;
        transform.localScale = originalScale * pulse;

        // Pulse light intensity
        if (portalLight != null)
            portalLight.intensity = 2f + Mathf.Sin(Time.time * pulseSpeed) * 1f;

        // Check if all dots collected
        if (requireAllDots)
        {
            Dot[] remainingDots = FindObjectsByType<Dot>(FindObjectsSortMode.None);
            playerCanExit = remainingDots.Length == 0;
        }
        else
        {
            playerCanExit = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (!playerCanExit)
        {
            // Tell player how many dots left
            Dot[] remainingDots = FindObjectsByType<Dot>(FindObjectsSortMode.None);
            Debug.Log($"Collect all coins first! {remainingDots.Length} remaining.");
            return;
        }

        Debug.Log("Portal reached! Level Complete!");

        if (GameAudioManager.Instance != null)
            GameAudioManager.Instance.PlayLevelComplete();

        GameManager.Instance.LoadNextLevel();
    }

    // Show portal radius in scene view
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, 1.5f);
    }
}