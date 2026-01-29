using System.Collections.Generic;
using UnityEngine;

public class ShopNPC : DialogNPC
{
    public ShopNPC() : base()
    {
        DialogOption trade = new DialogOption("What do you have to sell?", (Player player) =>
        {
            dialogManager.HideDialog();
            player.EnterShop();
        });


        dialog = new TextDialog("Hello, how are you?", new
            List<DialogOption>
        {
            new("I'm good, thanks!", (player) =>
            {
                TextDialog goodDialog = new("That's great to hear!", new List<DialogOption>
                {
                    new("Goodbye.", null),
                    trade
                });
                dialogManager.ShowDialog(goodDialog);
            }),
            new("Not so well.", (player) =>
            {
                TextDialog badDialog = new("I'm sorry to hear that.", new
                    List<DialogOption>
                {
                    new("Goodbye.", null),
                    trade
                });
                dialogManager.ShowDialog(badDialog);
            })
        });

    }
}

