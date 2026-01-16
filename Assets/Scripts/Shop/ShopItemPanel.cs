using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopItemPanel : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    private Item _item;
    public Item Item {
        get => _item;
        set
        {
            _item = value;
            FillOutItemFields();
            

        }
    }

    private TMP_Text itemNameText;
    private TMP_Text itemValueText;
    private Button itemAction;

    public UnityAction OnPressAction {
        set {
            if (itemAction == null)
            {
                itemAction = GameObject.Find("ShopItemBuyButton").GetComponent<Button>();

            }
            itemAction.onClick.RemoveAllListeners();
            itemAction.onClick.AddListener(value);
        }
    }

    
    private void FillOutItemFields()
    {
        if (_item == null) return;
        if (itemNameText == null)
        {
            itemNameText = GameObject.Find("ShopItemNameText").GetComponent<TMP_Text>();
        }
        if (itemValueText == null)
        {
            itemValueText = GameObject.Find("ShopItemValueText").GetComponent<TMP_Text>();
        }
        itemNameText.text = _item.Name;
        itemValueText.text = $"${_item.Value}";
    }

    void Start()
    {
    }
}
