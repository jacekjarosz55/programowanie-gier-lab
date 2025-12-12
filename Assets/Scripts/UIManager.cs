using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private ProgressBar healthBar;
    private ProgressBar staminaBar;
    private TMP_Text ammoText;
    private GameObject inventoryPanel;

    private float maxStamina;
    private float currentStamina;

    private float maxHealth;
    private float currentHealth;

    public float MaxHealth { get => maxHealth; set { maxHealth = value; UpdateHealth(); } }
    public float CurrentHealth { get => currentHealth; set { currentHealth = value; UpdateHealth(); } }
    public float MaxStamina { get => maxStamina; set { maxStamina = value; UpdateStamina(); } }
    public float CurrentStamina { get => currentStamina; set { currentStamina = value; UpdateStamina(); } }

    private bool _inventoryShown;
    public bool InventoryShown
    {
        get => _inventoryShown;
        set
        {
            _inventoryShown = value;
            inventoryPanel.SetActive(value);
        }
    }

    private int ammo;
    public int Ammo { get => ammo; set { ammo = value; UpdateAmmo(); } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthBar = GameObject.Find("HealthBar").GetComponent<ProgressBar>();
        staminaBar = GameObject.Find("StaminaBar").GetComponent<ProgressBar>();
        ammoText = GameObject.Find("AmmoText").GetComponent<TMP_Text>();
        inventoryPanel = GameObject.Find("InventoryPanel");
    }

    private void UpdateHealth()
    {
        if (healthBar == null || maxHealth == 0) return;
        healthBar.SetProgress(currentHealth/maxHealth);
    }
    private void UpdateStamina()
    {
        if (staminaBar == null || maxStamina == 0) return;
        staminaBar.SetProgress(currentStamina/maxStamina);
    }
    private void UpdateAmmo()
    {
        if (ammoText == null) return;
        ammoText.text = $"Ammo: {ammo}";
    }





}
