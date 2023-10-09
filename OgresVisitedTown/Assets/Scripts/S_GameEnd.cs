using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_GameEnd : MonoBehaviour
{
    private Script_PlayerMovement playerMovementRef;
    private S_playerInteractionManager playerInteractionRef;
    private S_uiManager uiManagerRef;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        playerMovementRef = FindObjectOfType<Script_PlayerMovement>();
        uiManagerRef = FindObjectOfType<S_uiManager>();
        playerInteractionRef = FindObjectOfType<S_playerInteractionManager>();
    }
    public void TriggerEndSequence(string pEndingText)
    {
        playerMovementRef.EnableDisableMovement();
        playerInteractionRef.DisableEnableInputs();
        uiManagerRef.EnableDisableEndingUI(pEndingText);
    }
    //stop player movement - DONE
    //turn on ending sequence UI - DONE
    //receive specific ending text
    //wait a couple of seconds
    //give options starting from checkpoint, from the start or exiting the game
    //If plays again, enable movement
    //Clean ending text
    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            TriggerEndSequence("lol");
        }
    }
}
