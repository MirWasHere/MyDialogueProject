using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueUi : MonoBehaviour
{
    
    [SerializeField] private GameObject dialogueBox;

    [SerializeField] private Image charSprite;
    [SerializeField] private Image drawingSprite;
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private TMP_Text nameText;

    // Holds the dialogue, will be used to determine the order
    private int[] dialoguePath;

    // What dialogue object you are on
    private int dialogueNum;

    private ResponseHandler responseHandler;

    // Holds the dialogue objects
    private Dialogue currDialogue;

    private TypeWritterEffect typeWritterEffect;

    // Start is called before the first frame update
    void Start()
    {
        typeWritterEffect = GetComponent<TypeWritterEffect>();

        responseHandler = GetComponent<ResponseHandler>();

        dialogueBox.SetActive(false);

        dialogueNum = 0;

        dialogueBox.SetActive(false);
    }

    // Displays dialogue in box
    public void DisplayDialogue(Dialogue dialogueObject)
    {
        // Sets the current dialouge to the inputed dialogue object
        currDialogue = dialogueObject;
        // Setting dialogue path's length
        dialoguePath = new int[currDialogue.sentenceTexts.Length];

        // Activating the dialogue box
        dialogueBox.SetActive(true);

        // Starting dialogue
        Debug.Log("Starting Dialogue");
        StartCoroutine(StepThroughDialogue(dialogueObject, 0));
    }

    // Made for the response choices; starts dialogue at certain point
    public void DisplayDialogue(int dialogueStart)
    {
        if(dialogueStart < 0)
        {
            CloseDialogueBox();
        }
        else
        {
        // Activating the dialogue box
        dialogueBox.SetActive(true);

        // Continuing dialogue
        Debug.Log("Restarting dialogue");
        StartCoroutine(StepThroughDialogue(currDialogue, dialogueStart));
        }
        
    }

    private IEnumerator StepThroughDialogue(Dialogue dialogueObject, int dialogueStart)
    {
        // Setting up sentence texts
        Dialogue.SentenceText[] sentenceTexts = dialogueObject.sentenceTexts;

        Debug.Log("Stepping through dialogue. Sentences: " + sentenceTexts.Length);
        dialogueNum = dialogueStart;
        // Going through each sentence
        for(; dialogueNum < sentenceTexts.Length; dialogueNum ++)
        {
            // To mark where the dialogue goes
            dialoguePath[dialogueNum] = 1;

            // Setting the sentence and name for dialogue
            string dialogue = sentenceTexts[dialogueNum].Sentences;
            Debug.Log(dialogue);
            nameText.text = sentenceTexts[dialogueNum].CharName;

            // Setting up the character portrait
            if(sentenceTexts[dialogueNum].CharSprite == null)
            {
                charSprite.color = new Color(0f, 0f, 0f, 0f);
            }
            else
            {
                charSprite.sprite = sentenceTexts[dialogueNum].CharSprite;
                charSprite.color = Color.white;
            }

            // If there's a supplemental drawing:
            if(sentenceTexts[dialogueNum].ArtPiece == null)
            {
                drawingSprite.color = new Color(0f, 0f, 0f, 0f);
            }
            else
            {
                drawingSprite.sprite = sentenceTexts[dialogueNum].ArtPiece;
                drawingSprite.color = Color.white;
            }


            // "Typing" the text
            yield return typeWritterEffect.Run(dialogue, textLabel);

            // Break if you're at the end of the text and if sentence texts has responses
            if(sentenceTexts[dialogueNum].Responses.Length > 0) break;

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }
        if(sentenceTexts[dialogueNum].Responses.Length > 0)
        {
            responseHandler.ShowResponses(sentenceTexts[dialogueNum].Responses);
        }
        else
        {
            CloseDialogueBox();
        }

    }


    private void CloseDialogueBox()
    {
        currDialogue = null;
        dialogueNum = 0;
        dialogueBox.SetActive(false);
        textLabel.text = "";    
        
        Debug.Log("Dialogue complete");
    }

}
