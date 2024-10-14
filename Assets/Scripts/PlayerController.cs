using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float xRange = 11.5f;
    private float zMin = 16.0f;
    private float zMax = 18.1f;
    private float maxSpeed = 5f;
    private float acceleration = 10f;
    private float deceleration = 5f;
    public GameObject projectilePrefab;

    private Vector3 velocity;

    void Update()
    {
        // Get input and reverse it
        float horizontalInput = -Input.GetAxis("Horizontal");
        float verticalInput = -Input.GetAxis("Vertical");


        // Calculate target velocity
        Vector3 targetVelocity = new Vector3(horizontalInput, 0f, verticalInput) * maxSpeed;

        // Apply acceleration or deceleration
        if (targetVelocity.magnitude > 0.1f)
        {
            velocity = Vector3.MoveTowards(velocity, targetVelocity, acceleration * Time.deltaTime);
        }
        else
        {
            velocity = Vector3.MoveTowards(velocity, Vector3.zero, deceleration * Time.deltaTime);
        }

        // Calculate new position
        Vector3 newPosition = transform.position + velocity * Time.deltaTime;

        // Clamp position within bounds
        newPosition.x = Mathf.Clamp(newPosition.x, -xRange, xRange);
        newPosition.z = Mathf.Clamp(newPosition.z, zMin, zMax);

        // Apply new position
        transform.position = newPosition;

        // Projectile logic (commented out for now)
        if (Input.GetKeyDown(KeyCode.Space))
        {
                Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
        }
    }
}