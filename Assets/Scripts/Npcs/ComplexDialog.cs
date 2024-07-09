using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCDialogueSystem : MonoBehaviour
{
    public string[] dialogueLines; 
    private int currentDialogueIndex = 0;
    private bool isInteracting = false;
    private bool isDisplaying = false;
    private bool hasMoreText = false;
    private int maxCharsPerPage = 42; 

    public TextMeshProUGUI dialogueText; 

    void Start()
    {
        
        dialogueText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isInteracting && isDisplaying)
        {
            if (Input.GetKeyDown(KeyCode.Space) && hasMoreText)
            {
                DisplayNextPage();
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                EndCurrentDialogue();
            }
        }
    }

    private void OnMouseDown()
    {
        if (!isInteracting)
        {
            isInteracting = true;
            DisplayNextDialogueLine();
        }
    }

    private void DisplayNextDialogueLine()
    {
        if (currentDialogueIndex < dialogueLines.Length)
        {
            string currentDialogue = dialogueLines[currentDialogueIndex];
            int length = currentDialogue.Length;

            if (length <= maxCharsPerPage)
            {
                
                dialogueText.text = currentDialogue;
                hasMoreText = false;
            }
            else
            {
                
                dialogueText.text = currentDialogue.Substring(0, maxCharsPerPage);
                hasMoreText = true;
            }

            dialogueText.gameObject.SetActive(true);
            isDisplaying = true;
            currentDialogueIndex++;
        }
        else
        {
            EndCurrentDialogue();
        }
    }

    private void DisplayNextPage()
    {
        string currentDialogue = dialogueLines[currentDialogueIndex - 1];
        int startIndex = dialogueText.text.Length;
        int remainingLength = currentDialogue.Length - startIndex;
        int charsToDisplay = Mathf.Min(remainingLength, maxCharsPerPage);

        dialogueText.text += currentDialogue.Substring(startIndex, charsToDisplay);

        if (startIndex + charsToDisplay >= currentDialogue.Length)
        {
            hasMoreText = false;
        }
    }

    private void EndCurrentDialogue()
    {
        dialogueText.gameObject.SetActive(false);
        isDisplaying = false;
        isInteracting = false;
        hasMoreText = false;

        
        if (currentDialogueIndex >= dialogueLines.Length)
        {
            currentDialogueIndex = 0;
        }
    }
}
