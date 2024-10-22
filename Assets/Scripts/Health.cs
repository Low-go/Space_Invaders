using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class Health : MonoBehaviour
{
    public Slider healthBar;
    public Image fillImage;
    public int maxHealth = 100;
    public ParticleSystem deathEffect;

    private int currentHealth;
    private GameManager gameManager;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
        gameManager = FindObjectOfType<GameManager>();
        fillImage.color = Color.red;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthBar.value = currentHealth;
        UpdateHealthBarColor();
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthBar.value = currentHealth;
        UpdateHealthBarColor();
    }

    private void UpdateHealthBarColor()
    {
        float healthPercentage = (float)currentHealth / maxHealth;
        fillImage.color = Color.Lerp(Color.white, Color.red, healthPercentage);
    }

    private void Die()
    {
        if (deathEffect != null)
        {
            ParticleSystem effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            effect.Play();
        }
        gameObject.SetActive(false); // Just disable the player
        gameManager.GameOver();
    }

    public void ResetHealth()
    {
        gameObject.SetActive(true); // Re-enable the player
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
        fillImage.color = Color.red;
    }
}