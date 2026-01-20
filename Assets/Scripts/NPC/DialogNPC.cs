using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class DialogNPC : MonoBehaviour, IInteractable
{
    public bool ShouldPickup => false;

    public GameObject GameObject => gameObject;

    public void OnActivate(Player player)
    {
        DialogOption trade = new DialogOption("What do you have to sell?", (Player player) =>
        {
            dialogManager.HideDialog();
            player.EnterShop();
        });




        var hiDialog = new TextDialog("Hello, how are you?", new
            List<DialogOption>
        {
            new DialogOption("I'm good, thanks!", (player) =>
            {
                var goodDialog = new TextDialog("That's great to hear!", new
                    List<DialogOption>
                {
                    new DialogOption("Goodbye.", null),
                    trade
                });
                dialogManager.ShowDialog(goodDialog);
            }),
            new DialogOption("Not so well.", (player) =>
            {
                var badDialog = new TextDialog("I'm sorry to hear that.", new
                    List<DialogOption>
                {
                    new DialogOption("Goodbye.", null),
                    trade
                });
                dialogManager.ShowDialog(badDialog);
            })
        });

        dialogManager.ShowDialog(hiDialog);
    }

    public void OnDeactivate(Player player)
    {
    }

    public void OnFocusEnter(Player player)
    {
    }

    public void OnFocusLeave(Player player)
    {
    }


    private DialogManager dialogManager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialogManager = GameObject.Find("DialogManager").GetComponent<DialogManager>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
