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
    private bool isWriting;
    private string currentTextString;
    private S_PlayerSpawnManager spawnManagerRef;

    //UI Interaction Elements
    [SerializeField] private Image portrait;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI guiSectionTextBox;
    [SerializeField] private GameObject dialogueWindow;
    [SerializeField] private GameObject interactionUI;

    //UI Ending Elements
    [SerializeField] private TextMeshProUGUI guiEndingTextBox;
    [SerializeField] private GameObject endingWindow;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        interactionsList = new List<S_interactionObject>();
        isInteracting = false;
        hasFinishedDialog = false;
        textAnimationSpeed = 0.5f;
        isWriting = false;
        spawnManagerRef = FindObjectOfType<S_PlayerSpawnManager>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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
        if (!isWriting)
        {
            currentTextString = pSentenceText;
            StartCoroutine(WriteTextToTextmesh(pSentenceText));
        }
        else
        {
            StopAllCoroutines();
        }
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
        isWriting = true;
        guiSectionTextBox.text = "";
        char[] _letters = _text.ToCharArray();

        float _speed = 1f - textAnimationSpeed;

        foreach (char _letter in _letters)
        {
            guiSectionTextBox.text += _letter;
            yield return new WaitForSeconds(0.1f * _speed);
        }
        isWriting = false;
    }
    public bool IsItWriting()
    {
        return isWriting;
    }
    public void PasteAllTextToTextmesh()
    {
        StopAllCoroutines();
        guiSectionTextBox.text = currentTextString;
        isWriting = false;
    }
    public void CloseDialogUI()
    {
        TurnDialogWindowOnOff();
        TurnInteractionUIOnOff();
        hasFinishedDialog = false;
    }
    public void EnableDisableEndingUI(string endingText)
    {
        switch (endingWindow.activeSelf)
        {
            case true:
                endingWindow.SetActive(false);
                SetEndingText("");
                break;
            case false:
                endingWindow.SetActive(true);
                SetEndingText(endingText);
                break;
        }
    }
    private void SetEndingText(string pText)
    {
        guiEndingTextBox.text = pText;
    }
    public void LoadFromCheckpoint()
    {
        endingWindow.SetActive(false);
        spawnManagerRef.SpawnPlayer();
    }
    private void LoadFromStart()
    {

    }
    private void ExitGame()
    {
        Debug.Log("me salgo");
    }
}


