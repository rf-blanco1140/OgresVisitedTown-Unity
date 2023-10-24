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
    private bool givenPermisionCrowbar;
    private bool obtainedCrowbar;
    private bool wasNotifiedOfBridge;

    //Events
    public delegate void CrowbarObatined();
    public static event CrowbarObatined EventCrowbarObtained;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        bool defaultValue = false;
        hasInspectedPotatos = defaultValue;
        botheredHouseA = defaultValue;
        botheredHouseB = defaultValue;
        botheredHouseC = defaultValue;
        caughtStealing = defaultValue;
        givenPermisionCrowbar = defaultValue;
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
    public bool GetPermitedToCrowbar()
    {
        return givenPermisionCrowbar;
    }
    public void NotifyPermitedToCrowbar()
    {
        givenPermisionCrowbar = true;
    }
    public bool GetObtainedCrowbar()
    {
        return obtainedCrowbar;
    }
    public void NotifyObtainedCrowbar()
    {
        obtainedCrowbar = true;
        if(EventCrowbarObtained != null)
        {
            EventCrowbarObtained();
        }
        Debug.Log("Consegui un Crowbar!!");
    }
    public bool HasTriggeredConsecuence(Consecuence pConsecuence)
    {
        bool answer = false;
        switch (pConsecuence)
        {
            case Consecuence.Potatos:
                answer = hasInspectedPotatos;
                break;
            case Consecuence.HouseA:
                answer = botheredHouseA;
                break;
            case Consecuence.HouseB:
                answer = botheredHouseB;
                break;
            case Consecuence.HouseC:
                answer = botheredHouseC;
                break;
            case Consecuence.Thief:
                answer = caughtStealing;
                break;
            case Consecuence.BridgeReady:
                answer = wasNotifiedOfBridge;
                break;
        }

        return answer;
    }
}

public enum Consecuence { None, Potatos, HouseA, HouseB, HouseC, Thief, CrowbarPermit, CrowbarCollection, BridgeReady }