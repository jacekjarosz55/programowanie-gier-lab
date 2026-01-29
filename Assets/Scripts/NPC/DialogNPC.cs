using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;

public class DialogNPC : MonoBehaviour, IInteractable
{

    public string nameOfNPC;
    public float initialHealth;
    public float initialMaxHealth;




    private string npcName;
    public string NPCName {
        get => npcName;
        set
        {
            npcName = value;
            if (npcName != null && npcNameText != null) {
                npcNameText.text = NPCName;
            }

        }
    }
    public bool ShouldPickup => false;

    public GameObject GameObject => gameObject;

    private float maxHealth; 
    public float MaxHealth
    {
        get => maxHealth;
        set
        {
            maxHealth = value;
            UpdateProgress();
        }
    }
    private float health; 
    public float Health
    {
        get => health;
        set
        {
            health = value;
            UpdateProgress();
        }
    }

    private void UpdateProgress()
    {
        if (healthBar == null) return;
        healthBar.SetProgress(health / maxHealth);
    }

    protected TextDialog dialog;
    public TMP_Text npcNameText;
    public ProgressBar healthBar;

    public DialogNPC()
    {
        dialog = new TextDialog("DEFAULT NPC DIALOG.", new List<DialogOption>{ new ("OK", null) });
    }


    public void OnActivate(Player player)
    {
        dialogManager.ShowDialog(dialog);
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


    protected DialogManager dialogManager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialogManager = GameObject.Find("DialogManager").GetComponent<DialogManager>();
        NPCName = nameOfNPC;
        MaxHealth = initialMaxHealth;
        Health = initialHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
