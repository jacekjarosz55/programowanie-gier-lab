using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HotbarSlot : MonoBehaviour
{

    public Image slotBackground;
    public TMP_Text itemNameText;
    public Color defaultColor = Color.black;
    
    private Item _item;
    public Item Item {
        get => _item;
        set
        {
            _item = value;
            if (value == null) {
                itemNameText.text =  "";
            }
            else
            {
                itemNameText.text = value.Name;
            }

        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
