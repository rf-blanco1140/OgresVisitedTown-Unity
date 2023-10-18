using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_playerInteractionManager : MonoBehaviour
{
    [SerializeField] private S_uiManager uiManagerRef;
    private S_interactionObject currentInteractable;
    private List<S_interactionObject> interactionsList;
    private bool isEnabeled;
    private Script_PlayerMovement playerMovementRef;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        interactionsList = new List<S_interactionObject>();
        isEnabeled = true;
        playerMovementRef = FindObjectOfType<Script_PlayerMovement>();
        uiManagerRef = FindObjectOfType<S_uiManager>();
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
        if(Input.GetKeyDown(KeyCode.Space) && isEnabeled)
        {
            if(currentInteractable != null)
            {
                bool isInteracting = currentInteractable.AttemptInteraction();
                if (isInteracting)
                {
                    playerMovementRef.EnableMovement(false);
                }
                else
                {
                    playerMovementRef.EnableMovement(true);
                }
            }
        }
    }
    private bool IsUiWriting()
    {
        return uiManagerRef.IsItWriting();
    }
    public void EnableInputs(bool pIsEnabled)
    {
        isEnabeled = pIsEnabled;
    }
}
