using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyMove : MonoBehaviour
{
    private float speed = 0.3f;
    private Transform player;

    void Start()
    {
        // Find the player's transform (assuming the player has a tag "Player")
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Move towards the player
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }
}
