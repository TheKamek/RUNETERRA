using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCDialogue : MonoBehaviour
{
    public string[] dialogues; // Array of dialogue strings
    private int dialogueIndex = 0;
    private bool isInteracting = false;

    public TextMeshProUGUI dialogueText; // Reference to the TextMeshProUGUI element

    void Start()
    {
        // Hide the dialogue text initially
        dialogueText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isInteracting && Input.GetKeyDown(KeyCode.Return))
        {
            DisplayNextDialogue();
        }
    }

    private void OnMouseDown()
    {
        if (!isInteracting)
        {
            isInteracting = true;
            DisplayNextDialogue();
        }
    }

    private void DisplayNextDialogue()
    {
        if (dialogueIndex < dialogues.Length)
        {
            dialogueText.text = dialogues[dialogueIndex];
            dialogueText.gameObject.SetActive(true);
            dialogueIndex++;
        }
        else
        {
            dialogueText.gameObject.SetActive(false);
            dialogueIndex = 0; // Reset dialogue index for replayability
            isInteracting = false;
        }
    }
}
