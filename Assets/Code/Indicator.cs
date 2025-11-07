using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour

{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;
    
    // Holds the character's dialogue [8/5: Indicator will hold the dialogue script within it]
    [SerializeField] private Dialogue dialogue;

    private bool playerInRange;

    private DialogueUi dialogueUI;


    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
        dialogueUI = GameObject.FindGameObjectWithTag("Canvas").GetComponent<DialogueUi>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInRange)
        {
            visualCue.SetActive(true);
            // Opens dialouge
            if(Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("E pressed");
                TriggerDialogue();
            }
        }
        else
        {
            visualCue.SetActive(false);
        }
    }

    // Triggering the dialogue
    // [8/6] It wasn't correctly going to the dialogue; separating may help?
    public void TriggerDialogue()
    {
        visualCue.SetActive(false);
        dialogueUI.DisplayDialogue(dialogue);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            playerInRange = true;

        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }
}
