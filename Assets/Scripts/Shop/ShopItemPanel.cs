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

    public TMP_Text itemNameText;
    public TMP_Text itemValueText;
    public Button itemAction;

    public UnityAction OnPressAction {
        set {
            itemAction.onClick.RemoveAllListeners();
            itemAction.onClick.AddListener(value);
        }
    }

    
    private void FillOutItemFields()
    {
        if (_item == null) return;
        itemNameText.text = _item.Name;
        itemValueText.text = $"${_item.Value}";
    }

    void Start()
    {
    }
}
