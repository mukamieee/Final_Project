using UnityEngine;

public class EnemyAILevel2 : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed = 4.5f;
    public float catchDistance = 0.8f;

    private Rigidbody rb;
    private Transform player;
    private bool hasCaught = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
        else
            Debug.LogError("Player not found! Make sure Player tag is set.");
    }

    void FixedUpdate()
    {
        if (player == null || hasCaught) return;
        if (GameManager.Instance != null && GameManager.Instance.isGameOver) return;

        // Chase player directly but faster than Level 1
        Vector3 direction = (player.position - transform.position).normalized;
        rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);

        // Face the player
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            rb.rotation = Quaternion.Slerp(
                rb.rotation,
                targetRotation,
                10f * Time.fixedDeltaTime);
        }

        // Check if caught player
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance < catchDistance)
        {
            hasCaught = true;

            if (GameAudioManager.Instance != null)
                GameAudioManager.Instance.PlayEnemyCatch();

            GameManager.Instance.GameOver();
        }
    }

    public void GetShot()
    {
        transform.position = new Vector3(20, 1, 20);
        hasCaught = false;
        Debug.Log("Enemy shot! Repositioned.");
    }
}