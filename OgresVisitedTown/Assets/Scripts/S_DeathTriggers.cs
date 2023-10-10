using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_DeathTriggers : MonoBehaviour
{
    private S_interactionConsecuencesManager consecuencesManagerRef;
    private S_GameEnd endingManagerRef;
    [SerializeField] private Consecuence myPrecondition;
    [SerializeField] private string endingText;
    //TODO add sound file to play when ending is triggered

    private void Start()
    {
        consecuencesManagerRef = FindObjectOfType<S_interactionConsecuencesManager>();
        endingManagerRef = FindObjectOfType<S_GameEnd>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (myPrecondition != Consecuence.None)
            {
                if(consecuencesManagerRef.HasTriggeredConsecuence(myPrecondition))
                {
                    TriggerEnding();
                }
            }
            else
            {
                TriggerEnding();
            }
        }
    }

    private void TriggerEnding()
    {
        endingManagerRef.TriggerEndSequence(endingText);
        //TODO play ending sound
    }
}
