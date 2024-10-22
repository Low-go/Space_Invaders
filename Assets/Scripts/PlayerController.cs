using System.Collections;
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
    public AudioClip shootSound;
    [Range(1f, 5f)]
    public float initialPlaybackSpeed = 2f;  // How fast to play the start of the sound
    public float minTimeBetweenShots = 0.1f;

    private AudioSource audioSource;
    private Vector3 velocity;
    private float nextShotTime = 0f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.playOnAwake = false;
        audioSource.loop = false;
        if (shootSound != null)
        {
            audioSource.clip = shootSound;
        }
    }

    void Update()
    {
        HandleMovement();
        HandleShooting();
    }

    void HandleMovement()
    {
        float horizontalInput = -Input.GetAxis("Horizontal");
        float verticalInput = -Input.GetAxis("Vertical");

        Vector3 targetVelocity = new Vector3(horizontalInput, 0f, verticalInput) * maxSpeed;

        if (targetVelocity.magnitude > 0.1f)
        {
            velocity = Vector3.MoveTowards(velocity, targetVelocity, acceleration * Time.deltaTime);
        }
        else
        {
            velocity = Vector3.MoveTowards(velocity, Vector3.zero, deceleration * Time.deltaTime);
        }

        Vector3 newPosition = transform.position + velocity * Time.deltaTime;
        newPosition.x = Mathf.Clamp(newPosition.x, -xRange, xRange);
        newPosition.z = Mathf.Clamp(newPosition.z, zMin, zMax);
        transform.position = newPosition;
    }

    void HandleShooting()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time >= nextShotTime)
        {
            Shoot();
            nextShotTime = Time.time + minTimeBetweenShots;
        }
    }

    void Shoot()
    {
        if (projectilePrefab != null)
        {
            Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
        }

        if (shootSound != null)
        {
    
            AudioSource tempSource = gameObject.AddComponent<AudioSource>();
            tempSource.clip = shootSound;
            tempSource.pitch = initialPlaybackSpeed;  // Increase playback speed
            tempSource.Play();


            StartCoroutine(CleanupTempAudio(tempSource));
        }
    }

    IEnumerator CleanupTempAudio(AudioSource tempSource)
    {
        float duration = shootSound.length / initialPlaybackSpeed;
        yield return new WaitForSeconds(duration);
        Destroy(tempSource);
    }
}