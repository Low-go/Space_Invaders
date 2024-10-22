using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollision : MonoBehaviour
{
    public string objectType;
    public ParticleSystem selfBoom;
    private int damage = 25;
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene. Make sure it is present.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        switch (objectType)
        {
            case "Enemy":
                HandleEnemyCollision(other);
                break;
            case "Projectile":
                HandleProjectileCollision(other);
                break;
            default:
                Debug.LogWarning($"Unknown object type: {objectType} on {gameObject.name}");
                break;
        }
    }

    void HandleEnemyCollision(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Health playerHealth = other.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
            Debug.Log($"Enemy {gameObject.name} hit player. Destroying enemy.");

            // Create and explicitly play the effect
            ParticleSystem explosionInstance = Instantiate(selfBoom, transform.position, Quaternion.identity);
            Debug.Log("Collision detected with: " + other.gameObject.name);
            explosionInstance.Play(); // Add this line

            Destroy(gameObject);
            Destroy(explosionInstance.gameObject, explosionInstance.main.duration);
        }
    }

    void HandleProjectileCollision(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Particle effect should be playing.");

            // Instantiate and play the explosion effect
            ParticleSystem explosionInstance = Instantiate(selfBoom, transform.position, Quaternion.identity);
            explosionInstance.Play();

            if (gameManager != null)
            {
                gameManager.IncreaseScore(25);
            }

            // Destroy both the enemy and the projectile
            Destroy(other.gameObject);
            Destroy(gameObject);

            // Schedule destruction of the particle system after it finishes
            Destroy(explosionInstance.gameObject, explosionInstance.main.duration);
        }
    }
}
