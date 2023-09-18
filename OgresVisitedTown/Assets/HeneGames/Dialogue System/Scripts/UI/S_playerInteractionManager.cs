using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_playerInteractionManager : MonoBehaviour
{
    [SerializeField] private S_uiManager uiManagerRef;
    private S_interactionObject currentInteractable;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        S_interactionObject pInterObject = collision.GetComponent<S_interactionObject>();
        if (pInterObject)
        {
            uiManagerRef.InteractionRegistereed(pInterObject);
            currentInteractable = pInterObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        S_interactionObject pInterObject = collision.GetComponent<S_interactionObject>();
        if (pInterObject)
        {
            uiManagerRef.InteractionUnregistered(pInterObject);
            currentInteractable = null;
        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
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
    }
}
