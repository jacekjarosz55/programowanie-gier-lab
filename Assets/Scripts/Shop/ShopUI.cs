using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{

    public GameObject shopItemPanel;

    public Action<Item> onBuyClicked;



    private List<Item> items = new()
    {
        new AmmoPackItem()
        {
            Name = "Ammo Pack (20)",
            Value = 40
        },
        new PoisonItem()
        {
            Name = "Poison (-20)",
            Value = 30
        },
    };

    private GameObject shopViewportContent;


    private void OnPress(Item item)
    {
        if (onBuyClicked != null)
        {
            onBuyClicked(item);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        shopViewportContent = GameObject.Find("ShopContent");

        foreach (var item in items)
        {
            GameObject shopItem = Instantiate(shopItemPanel, shopViewportContent.transform);
            ShopItemPanel shopItemComponent  = shopItem.GetComponent<ShopItemPanel>();  
            shopItemComponent.Item = item;
            shopItemComponent.OnPressAction = () => OnPress(item); 
        }

        


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
