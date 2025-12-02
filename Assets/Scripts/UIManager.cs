using UnityEngine;

public class UIManager : MonoBehaviour
{
    private ProgressBar healthBar;
    private float maxHealth;
    private float currentHealth;

    public float MaxHealth { get => maxHealth; set { maxHealth = value; UpdateHealth(); } }
    public float CurrentHealth { get => currentHealth; set { currentHealth = value; UpdateHealth(); } }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void UpdateHealth()
    {
        healthBar.SetProgress(currentHealth/maxHealth);
        Debug.Log($"Health updated: {currentHealth}/{maxHealth}");
    }

    void Start()
    {
        healthBar = GameObject.Find("HealthBar").GetComponent<ProgressBar>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
