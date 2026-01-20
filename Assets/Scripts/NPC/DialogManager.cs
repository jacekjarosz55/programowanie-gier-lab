using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;




public class DialogManager : MonoBehaviour
{


    public GameObject actionButtonPrefab;
    private Player player;
    private GameObject dialogBox;
    private TMP_Text dialogText;
    private Transform actionGroup;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        dialogBox = GameObject.Find("DialogBox");
        dialogText = dialogBox.transform.Find("DialogText").GetComponent<TMP_Text>();
        actionGroup = dialogBox.transform.Find("ActionGroup");
        HideDialog();
    }

    public void ShowDialog(TextDialog dialog)
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        player.InDialog = true;
        dialogBox.SetActive(true);
        dialogText.text = dialog.text;

        foreach (Transform child in actionGroup)
        {
            Destroy(child.gameObject);
        }

        foreach (DialogOption option in dialog.options)
        {
           var actionButton = Instantiate(actionButtonPrefab, actionGroup);
           actionButton.GetComponentInChildren<TMP_Text>().text = option.text;
           actionButton.GetComponent<Button>().onClick.AddListener(() => 
           {
               if (option.action != null)
               {
                   option.action(player);
               }
               else
               {
                     HideDialog();
               }
           });
        }
    }

    public void HideDialog()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        dialogBox.SetActive(false);
        player.InDialog = false;

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
