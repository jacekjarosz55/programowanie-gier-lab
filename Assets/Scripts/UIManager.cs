using System;
using System.Collections.Generic;
using System.Linq;
using Mono.Cecil;
using TMPro;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;

public class UIManager : MonoBehaviour
{


    public Action<Item> onItemSold;
    public Action<Item> onItemBought;
    public Action<Item,int> onItemAssign;

    private HotbarSlot[] hotbarSlots;

    private ProgressBar healthBar;
    private ProgressBar staminaBar;
    private TMP_Text ammoText;
    public GameObject inventoryPanel;
    public GameObject shopPanel;
    public GameObject gameOverScreen;

    //private TMP_Text inventoryContentText;
    public RectTransform inventoryContent;
    public GameObject inventoryItemEntry;
    private TMP_Text combinedValueText;
    private TMP_Text cashText;

    private float maxStamina;
    private float currentStamina;

    private float maxHealth;
    private float currentHealth;

    private int cash = 0;
    public int Cash { get => cash; set { cash = value; UpdateCash(); } }

    public float MaxHealth { get => maxHealth; set { maxHealth = value; UpdateHealth(); } }
    public float CurrentHealth { get => currentHealth; set { currentHealth = value; UpdateHealth(); } }
    public float MaxStamina { get => maxStamina; set { maxStamina = value; UpdateStamina(); } }
    public float CurrentStamina { get => currentStamina; set { currentStamina = value; UpdateStamina(); } }



    private void UpdateCash()
    {
        cashText.text = $"Cash: ${cash}";
    }

    private List<Item> _inventory;

    public List<Item> Inventory
    {
        get => _inventory;
        set
        {
            _inventory = value;
        }
    }

    private Item[] _hotbarItems = new Item[4];

    public Item[] HotbarItems
    {
        get => _hotbarItems;
        set
        {
            _hotbarItems = value;
            UpdateHotbar();
        }
    }


    private void UpdateHotbar()
    {
        for (int i = 0; i < hotbarSlots.Length; i++)
        {
            hotbarSlots[i].Item = _hotbarItems[i];
        }
    }

    private bool _inShop = false;
    public bool InShop
    {
        get => _inShop;
        set {
            _inShop = value;
            shopPanel.SetActive(value);
            shopPanel.GetComponent<ShopUI>().onBuyClicked = (Item item) =>
            {
                if (onItemBought != null) {
                    onItemBought(item);
                }
            };
            
            InventoryShown = value;
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


            if (_inventoryShown)
            {
                ShowMouse();
            }
            else
            {
                HideMouse();
            }
        }
    }
    
    private void HideMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void ShowMouse()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    private int ammo;
    public int Ammo { get => ammo; set { ammo = value; UpdateAmmo(); } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthBar = GameObject.Find("HealthBar").GetComponent<ProgressBar>();
        staminaBar = GameObject.Find("StaminaBar").GetComponent<ProgressBar>();
        ammoText = GameObject.Find("AmmoText").GetComponent<TMP_Text>();

        hotbarSlots = new HotbarSlot[4];
        for (int i = 0; i < 4; i++)
        {
            hotbarSlots[i] = GameObject.Find($"HotbarSlot{i+1}").GetComponent<HotbarSlot>();
        }
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



    public void ForceInventoryUpdate()
    {
        UpdateInventoryContent();
    }

    private void UpdateInventoryContent() {
        //inventoryContentText = GameObject.Find("InventoryContentText").GetComponent<TMP_Text>();
        foreach (RectTransform entry in inventoryContent)
        {
            Destroy(entry.gameObject);
        }

        combinedValueText = GameObject.Find("CombinedValueText").GetComponent<TMP_Text>();
        cashText = GameObject.Find("CashText").GetComponent<TMP_Text>();
        //inventoryContentText.text = "";
        //inventoryContentText.text += string.Join('\n', Inventory.Select(x=>$"{x.Name}  ---  ${x.Value}"));
        /*
        while(inventoryContent.childCount > 0) {
            var entry = inventoryContent.GetChild(0);
            Destroy(entry);
        }
        */

        foreach (var item in Inventory)
        {
            var entryItem = item;
            GameObject entryObj = Instantiate(inventoryItemEntry, inventoryContent);
            var entry = entryObj.GetComponent<InventoryItemEntry>();
            entry.onSell = (item) => {
                onItemSold(item);
            };
            entry.onSlotAssigned = (item, slot) =>
            {
                onItemAssign(item, slot);
            };
            entry.item = entryItem;
            entry.sellingEnabled = InShop;
            entry.Initialize();
        }

        float combinedValue = Inventory.Sum(x=>x.Value);
        combinedValueText.text = $"Combined Value: ${combinedValue + cash}";
    }

    public void ShowGameOver()
    {
        gameOverScreen.SetActive(true);
    }
}
