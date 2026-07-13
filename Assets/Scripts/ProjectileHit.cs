using UnityEngine;

public class ProjectileHit : MonoBehaviour
{
    [Header("Settings")]
    public int pointsOnHit = 20;

    void OnCollisionEnter(Collision collision)
    {
        // Hit enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy hit! +" + pointsOnHit + " points");

            // Add bonus points
            if (ScoreManager.Instance != null)
                ScoreManager.Instance.AddPoints(pointsOnHit);

            // Reset enemy position
            EnemyAI enemy = collision.gameObject.GetComponent<EnemyAI>();
            if (enemy != null)
                enemy.GetShot();

            // Also check for Level 2 enemy
            EnemyAILevel2 enemy2 = collision.gameObject.GetComponent<EnemyAILevel2>();
            if (enemy2 != null)
                enemy2.GetShot();

            Destroy(gameObject);
        }
        // Hit wall or ground — just destroy
        else if (!collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}