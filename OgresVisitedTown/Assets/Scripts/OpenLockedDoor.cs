using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenLockedDoor : MonoBehaviour
{
    [SerializeField] GameObject door;
    [SerializeField] private S_interactionConsecuencesManager consecuencesManagerRef;
    private bool canOpenDoor;

    private void Start()
    {
        consecuencesManagerRef = FindObjectOfType<S_interactionConsecuencesManager>();
    }

    private void Update()
    {
        canOpenDoor = consecuencesManagerRef.GetWasNotifiedOfCrowbar();
        if(canOpenDoor)
        {
            door.SetActive(false);
        }
    }
}
