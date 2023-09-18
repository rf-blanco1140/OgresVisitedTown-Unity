using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class S_uiManager : MonoBehaviour
{
    private List<S_interactionObject> interactionsList;
    private bool isInteracting;
    private bool hasFinishedDialog;
    [SerializeField] private float textAnimationSpeed;

    //UI Elements
    [SerializeField] private Image portrait;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI guiSectionTextBox;
    [SerializeField] private GameObject dialogueWindow;
    [SerializeField] private GameObject interactionUI;

    private void Start()
    {
        interactionsList = new List<S_interactionObject>();
        isInteracting = false;
        hasFinishedDialog = false;
        textAnimationSpeed = 0.5f;
    }
    public void InteractionRegistereed(S_interactionObject interObject)
    {
        interactionsList.Add(interObject);
        TurnInteractionUIOnOff();
    }
    public void InteractionUnregistered(S_interactionObject interObject)
    {
        interactionsList.Remove(interObject);
        TurnInteractionUIOnOff();
    }
    public void AttemptInteraction(string pSentenceText)
    {
        TurnDialogWindowOnOff();
        StartCoroutine(WriteTextToTextmesh(pSentenceText));
    }
    private void TurnInteractionUIOnOff()
    {
        if(interactionsList.Count>0 && !isInteracting)
        {
            interactionUI.SetActive(true);
        }
        else
        {
            interactionUI.SetActive(false);
        }
    }
    private void TurnDialogWindowOnOff()
    {
        if(interactionsList.Count > 0 && !hasFinishedDialog)
        {
            isInteracting = true;
            TurnInteractionUIOnOff();
            dialogueWindow.SetActive(true);
        }
        else
        {
            isInteracting = false;
            dialogueWindow.SetActive(false);
            hasFinishedDialog = false;
        }
    }
    public void NotifyInteractionEnd(bool pAnswer)
    {
        hasFinishedDialog = pAnswer;
    }
    IEnumerator WriteTextToTextmesh(string _text)
    {
        guiSectionTextBox.text = "";
        char[] _letters = _text.ToCharArray();

        float _speed = 1f - textAnimationSpeed;

        foreach (char _letter in _letters)
        {
            guiSectionTextBox.text += _letter;
            yield return new WaitForSeconds(0.1f * _speed);
        }
    }
    public void CloseDialogUI()
    {
        TurnDialogWindowOnOff();
        TurnInteractionUIOnOff();
        hasFinishedDialog = false;
    }
}


