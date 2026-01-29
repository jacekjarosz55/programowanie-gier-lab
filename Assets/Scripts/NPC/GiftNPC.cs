using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;

public class GiftNPC : DialogNPC
{

    private HealthItem healthItem = new()
    {
        Name = "Medkit (+20)",
        Value = 40
    };
    private AmmoPackItem ammoPackItem =
        new AmmoPackItem()
        {
            Name = "Ammo Pack (20)",
            Value = 40
        };

    public GiftNPC() : base()
    {
        dialog = new TextDialog("Hello, you look like you need something from me.", new
            List<DialogOption>
        {
            new("Give me health", (player) =>
            {
                player.AddItem(healthItem);
                dialogManager.HideDialog();
            }),
            new("Give me ammo", (player) =>
            {
                player.AddItem(ammoPackItem);
                dialogManager.HideDialog();
            }),
        });

    }
}

