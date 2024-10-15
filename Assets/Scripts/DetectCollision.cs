using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollision : MonoBehaviour
{
    
    public string objectType;
    public ParticleSystem selfBoom;

    void Start()
    {
        
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
            Debug.Log($"Enemy {gameObject.name} hit player. Destroying enemy.");
            ParticleSystem explosionInstance = Instantiate(selfBoom, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        // I dont think I need this
        //else if (other.CompareTag("Projectile"))
        //{
          
        //    selfBoom.Play();

        //    //Destroy(other.gameObject); // boom projectile
        //    //Destroy(gameObject); // boom enemy
        //}
    }

    void HandleProjectileCollision(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {

            Debug.Log("Particle effect should be playing.");
            ParticleSystem explosionInstance = Instantiate(selfBoom, transform.position, Quaternion.identity);
            explosionInstance.Play();
            Destroy(other.gameObject); // oil enemy up
            Destroy(gameObject); // oil projectile up
        }
        // Projectile doesn't do anything when hitting the player
    }
}