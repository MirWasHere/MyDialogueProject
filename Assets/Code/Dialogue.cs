using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [SerializeField] public SentenceText[] sentenceTexts;
    
    //bool openDialogue;

    public DialogueUi dialogueUI;


    /***   [8/4]: DIALOGUE DOESN'T DO ANYTHING??? IT'S JUST A CONTAINER DX   ***/


    // Start is called before the first frame update
    void Start()
    {
        //openDialogue = false;
        dialogueUI = GameObject.FindGameObjectWithTag("Canvas").GetComponent<DialogueUi>();
    }



/*** Sentence and response objects ***/
    [System.Serializable]
    public class SentenceText
    {
        [TextArea(3, 10)]
        [SerializeField] private string sentence;
        [SerializeField] private string charName;
        [SerializeField] private Sprite drawing;
        [SerializeField] private Sprite charSprite;

        // Giving each sentence an option for response text instead
        [SerializeField] private Response[] responses;

        public string Sentences => sentence;
        public string CharName => charName;
        public Sprite ArtPiece => drawing;
        public Sprite CharSprite => charSprite;

        public Response[] Responses => responses;

        public void setCharName(string name) {

            charName = name;

        }

        public void setCharSprite(Sprite sprite) {

            charSprite = sprite;

        }

    }

    [System.Serializable]
    public class Response
    {
        [SerializeField] private string responseText;
        // Holds the number representing the dialogue choice
        [SerializeField] private int nextDialogue;

        public string ResponseText => responseText;

        public int NextDialogue => nextDialogue;
    }

}
