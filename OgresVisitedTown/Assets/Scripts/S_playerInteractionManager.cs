using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_playerInteractionManager : MonoBehaviour
{
    [SerializeField] private S_uiManager uiManagerRef;
    private S_interactionObject currentInteractable;
    private List<S_interactionObject> interactionsList;

    private void Start()
    {
        interactionsList = new List<S_interactionObject>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        S_interactionObject pInterObject = collision.GetComponent<S_interactionObject>();
        if (pInterObject)
        {
            interactionsList.Add(pInterObject);
            uiManagerRef.InteractionRegistereed(pInterObject);
            currentInteractable = interactionsList[0];
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        S_interactionObject pInterObject = collision.GetComponent<S_interactionObject>();
        if (pInterObject)
        {
            interactionsList.Remove(pInterObject);
            uiManagerRef.InteractionUnregistered(pInterObject);
            if(interactionsList.Count>0)
            {
                currentInteractable = interactionsList[0];
            }
            else
            {
                currentInteractable = null;
            }
        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (!IsUiWriting())
            {
                bool isEndOfInteraction = currentInteractable.IsEndOfSection();
                uiManagerRef.NotifyInteractionEnd(isEndOfInteraction);
                if (isEndOfInteraction == false)
                {
                    uiManagerRef.AttemptInteraction(currentInteractable.GetCurrentSentence());
                }
                else
                {
                    uiManagerRef.CloseDialogUI();
                }
            }
            else
            {
                uiManagerRef.PasteAllTextToTextmesh();
            }
        }
    }
    private bool IsUiWriting()
    {
        return uiManagerRef.IsItWriting();
    }
}