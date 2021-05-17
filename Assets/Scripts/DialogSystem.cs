using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    public static DialogSystem Instance { get; set; }

    public List<string> DialogLines;
    public GameObject NPCDialoguePanel;

    Button dialogButton;
    Text currentDialog, NPCName, buttonText;
    int dialogIndex;
    void Awake()
    {
        NPCName = NPCDialoguePanel.transform.Find("NPCName").GetChild(0).GetComponent<Text>();
        currentDialog = NPCDialoguePanel.transform.Find("Dialog").GetComponent<Text>();
        dialogButton = NPCDialoguePanel.transform.Find("NextButton").GetComponent<Button>();
        dialogButton.onClick.AddListener(delegate { OnDialogButtonClicked(); });
        buttonText = dialogButton.transform.Find("Text").GetComponent<Text>();
        NPCDialoguePanel.SetActive(false);

        if (Instance && Instance!=this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void AddNewDialog(string[] lines, string npc)
    {
        dialogIndex = 0;
        DialogLines = new List<string>(lines.Length);
        DialogLines.AddRange(lines);
        NPCName.text = npc;
        SetDialog();
        NPCDialoguePanel.SetActive(true);
    }

    public void SetDialog()
    {
        currentDialog.text = DialogLines[dialogIndex];
        if (dialogIndex == DialogLines.Count-1)
        {
            buttonText.text = "OK";
        }
        else
        {
            buttonText.text = "Continue";
        }
    }

    public void OnDialogButtonClicked()
    {
        if (dialogIndex == DialogLines.Count-1)
        {
            NPCDialoguePanel.SetActive(false);
            return;
        }

        dialogIndex++;
        SetDialog();
    }
}
