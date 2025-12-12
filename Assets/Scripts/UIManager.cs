using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private ProgressBar healthBar;
    private TMP_Text ammoText;


    private float maxHealth;
    private float currentHealth;

    public float MaxHealth { get => maxHealth; set { maxHealth = value; UpdateHealth(); } }
    public float CurrentHealth { get => currentHealth; set { currentHealth = value; UpdateHealth(); } }

    private int ammo;
    public int Ammo { get => ammo; set { ammo = value; UpdateAmmo(); } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthBar = GameObject.Find("HealthBar").GetComponent<ProgressBar>();
        ammoText = GameObject.Find("AmmoText").GetComponent<TMP_Text>();
    }

    private void UpdateHealth()
    {
        if (healthBar == null || maxHealth == 0) return;
        healthBar.SetProgress(currentHealth/maxHealth);
    }
    private void UpdateAmmo()
    {
        if (ammoText == null) return;
        ammoText.text = $"Ammo: {ammo}";
    }




}
