using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemEntry : MonoBehaviour
{
    public Action<Item, int> onSlotAssigned;
    public Action<Item> onSell;
    public bool sellingEnabled = false;
    public TMP_Text itemName;
    public TMP_Text itemValue;
    public Button sellButton;
    public List<Button> slotButtons;
    public Item item;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    public void Initialize()
    {

        itemName.text = $"{item.Name}";
        itemValue.text = $"${item.Value}";
        sellButton.interactable = sellingEnabled;
        sellButton.onClick.AddListener(() => onSell(item));
        for (int i = 0; i < 4; i++)
        {
            int btn = i;
            slotButtons[i].onClick.AddListener(() =>
            {
                Debug.Log($"[item entry] {item.Name} -> {btn}");
                onSlotAssigned(item, btn);
            });
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
