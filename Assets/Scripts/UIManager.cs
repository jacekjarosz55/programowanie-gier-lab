using UnityEngine;

public class UIManager : MonoBehaviour
{
    private ProgressBar healthBar;
    private float maxHealth;
    private float currentHealth;

    public float MaxHealth { get => maxHealth; set { maxHealth = value; UpdateHealth(); } }
    public float CurrentHealth { get => currentHealth; set { currentHealth = value; UpdateHealth(); } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthBar = GameObject.Find("HealthBar").GetComponent<ProgressBar>();
    }

    private void UpdateHealth()
    {
        if (healthBar == null || maxHealth == 0) return;
        healthBar.SetProgress(currentHealth/maxHealth);
        Debug.Log($"Health updated: {currentHealth}/{maxHealth}");
    }

}
