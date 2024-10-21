using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Slider healthBar; // Reference to the Slider
    public Image fillImage;  // Reference to the Fill Image
    public int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
        fillImage.color = Color.red;  // Start with red
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthBar.value = currentHealth;

        UpdateHealthBarColor();

        if (currentHealth <= 0)
        {
            // Call game over or player death logic here
            Debug.Log("Player is dead");
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthBar.value = currentHealth;

        UpdateHealthBarColor();
    }

    // This method adjusts the color of the fill as health decreases
    private void UpdateHealthBarColor()
    {
        // Interpolate from red (full health) to white (zero health)
        float healthPercentage = (float)currentHealth / maxHealth;
        fillImage.color = Color.Lerp(Color.white, Color.red, healthPercentage);
    }
}
