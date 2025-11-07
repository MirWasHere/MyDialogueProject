using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class ResponseHandler : MonoBehaviour
{
    [SerializeField] private RectTransform responseBox;
    [SerializeField] private RectTransform responseButtonTemplate;
    [SerializeField] private RectTransform responseContainer;
    
    private DialogueUi dialogueUI;

    private List<GameObject> tempResponseButtons = new List<GameObject>();


    // Start is called before the first frame update
    private void Start()
    {
        dialogueUI = GetComponent<DialogueUi>();

    }

    
    public void ShowResponses(Dialogue.Response[] responses)
    {
        float responseBoxHeight = 0;

        foreach(Dialogue.Response response in responses)
        {
            GameObject responseButton = Instantiate(responseButtonTemplate.gameObject, responseContainer);
            responseButton.gameObject.SetActive(true);
            responseButton.GetComponent<TMP_Text>().text = response.ResponseText;
            responseButton.GetComponent<Button>().onClick.AddListener (()=> OnPickedResponse(response));

            tempResponseButtons.Add(responseButton);
            
            responseBoxHeight += responseButtonTemplate.sizeDelta.y;
        }

        responseBox.sizeDelta = new Vector2(responseBox.sizeDelta.x, responseBoxHeight);
        responseBox.gameObject.SetActive(true);
        Debug.Log("Setup complete!");
    }



    private void OnPickedResponse(Dialogue.Response response)
    {
        Debug.Log("Clicked!");
        responseBox.gameObject.SetActive(false);

        foreach(GameObject button in tempResponseButtons)
        {
            Destroy(button);
        }
        tempResponseButtons.Clear();

        dialogueUI.DisplayDialogue(response.NextDialogue);
    }


    public void ClearResponses() 
    {
        responseBox.gameObject.SetActive(false);

        foreach(GameObject button in tempResponseButtons)
        {
            Destroy(button);
        }
        tempResponseButtons.Clear();
    }

}
