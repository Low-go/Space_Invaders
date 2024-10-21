using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollision : MonoBehaviour
{
    public string objectType;
    public ParticleSystem selfBoom;
    private int damage = 10;
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
                playerHealth.TakeDamage(damage); // Damage player upon impact
            }
            Debug.Log($"Enemy {gameObject.name} hit player. Destroying enemy.");
            ParticleSystem explosionInstance = Instantiate(selfBoom, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    void HandleProjectileCollision(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Particle effect should be playing.");
            ParticleSystem explosionInstance = Instantiate(selfBoom, transform.position, Quaternion.identity);
            explosionInstance.Play();

          
            if (gameManager != null)
            {
                gameManager.IncreaseScore(25); 
            }

            Destroy(other.gameObject); // Destroy the enemy
            Destroy(gameObject); // Destroy the projectile
        }
        // Projectile doesn't do anything when hitting the player
    }
}
