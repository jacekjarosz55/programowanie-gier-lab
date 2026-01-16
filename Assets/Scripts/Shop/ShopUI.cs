using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{

    public GameObject shopItemPanel;

    private List<Item> items = new()
    {
        new() {
            Name = "Special object",
            Value = 200
        },
        new() {
            Name = "cheap idk wtf object",
            Value = 20
        }
    };

    private GameObject shopViewportContent;


    private void OnPress(Item item)
    {
        Debug.Log($"Pressed on item: {item.Name}, value: {item.Value}");
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
            shopItemComponent.OnPressAction = () => OnPress(item); }

        


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
