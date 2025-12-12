using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    private ProgressBar healthBar;
    private ProgressBar staminaBar;
    private TMP_Text ammoText;
    public GameObject inventoryPanel;

    private TMP_Text inventoryContentText;
    private TMP_Text combinedValueText;

    private float maxStamina;
    private float currentStamina;

    private float maxHealth;
    private float currentHealth;

    public float MaxHealth { get => maxHealth; set { maxHealth = value; UpdateHealth(); } }
    public float CurrentHealth { get => currentHealth; set { currentHealth = value; UpdateHealth(); } }
    public float MaxStamina { get => maxStamina; set { maxStamina = value; UpdateStamina(); } }
    public float CurrentStamina { get => currentStamina; set { currentStamina = value; UpdateStamina(); } }



    private List<Item> _inventory;
    public List<Item> Inventory
    {
        get => _inventory;
        set
        {
            _inventory = value;
        }
    }

    private bool _inventoryShown = false;
    public bool InventoryShown
    {
        get => _inventoryShown;
        set
        {
            _inventoryShown = value;
            inventoryPanel.SetActive(value);
            if (InventoryShown && Inventory != null) {
                UpdateInventoryContent();
            }
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



    private void UpdateInventoryContent() {
        inventoryContentText = GameObject.Find("InventoryContentText").GetComponent<TMP_Text>();
        combinedValueText = GameObject.Find("CombinedValueText").GetComponent<TMP_Text>();
        inventoryContentText.text = "";

        float combinedValue = Inventory.Sum(x=>x.Value);

        inventoryContentText.text += string.Join('\n', Inventory.Select(x=>$"{x.Name} (${x.Value})"));
        combinedValueText.text = $"Combined Value: ${combinedValue}";
    }

}
