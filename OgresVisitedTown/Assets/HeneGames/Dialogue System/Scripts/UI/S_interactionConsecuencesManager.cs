using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_interactionConsecuencesManager : MonoBehaviour
{
    private bool hasInspectedPotatos;
    private bool botheredHouseA;
    private bool botheredHouseB;
    private bool botheredHouseC;
    private bool caughtStealing;
    private bool wasNotifiedOfBridge;

    private void Start()
    {
        bool defaultValue = false;
        hasInspectedPotatos = defaultValue;
        botheredHouseA = defaultValue;
        botheredHouseB = defaultValue;
        botheredHouseC = defaultValue;
        caughtStealing = defaultValue;
        wasNotifiedOfBridge = defaultValue;
    }
    public bool GetHasInespectedPotatos()
    {
        return hasInspectedPotatos;
    }
    public void NotifyInspectedPotatos()
    {
        hasInspectedPotatos = true;
    }
    public bool GetBotheredHouseA()
    {
        return botheredHouseA;
    }
    public void NotifyBotehredHouseA()
    {
        botheredHouseA = true;
    }
    public bool GetBotheredHouseB()
    {
        return botheredHouseB;
    }
    public void NotifyBotehredHouseB()
    {
        botheredHouseB = true;
    }
    public bool GetBotheredHouseC()
    {
        return botheredHouseC;
    }
    public void NotifyBotehredHouseC()
    {
        botheredHouseC = true;
    }
    public bool GetCaughtStealing()
    {
        return caughtStealing;
    }
    public void NotifyStealing()
    {
        caughtStealing = true;
    }
    public bool GetWasNotifiedOfBridge()
    {
        return wasNotifiedOfBridge;
    }
    public void NotifyBridgeReady()
    {
        wasNotifiedOfBridge = true;
    }
}
