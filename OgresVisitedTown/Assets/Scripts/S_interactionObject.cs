using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class S_interactionObject : MonoBehaviour
{
    protected int currentSectionID;
    protected int currentSentenceID;
    protected string[] currentSentences;
    [SerializeField] protected S_interactionConsecuencesManager consecuencesManagerRef;
    [SerializeField] protected Consecuence iTriggerConsecuence;
    [SerializeField] protected Consecuence iReactToConsecuence;
    [SerializeField] protected bool reactsToExclusiveConsecuence;
    private S_uiManager uiManagerRef;

    [Header("Dialogue")]
    [SerializeField] protected List<DialogTextContainer> sections = new List<DialogTextContainer>();

    private void Start()
    {
        currentSectionID = 0;
        currentSentenceID = 0;
        consecuencesManagerRef = FindObjectOfType<S_interactionConsecuencesManager>();
        uiManagerRef = FindObjectOfType<S_uiManager>();
    }
    public virtual string GetCurrentSentence()
    {
        //if reacts to exclusive consecuences and triggered their conditions, replace sections list with the corresponding sections list, set the IDs to 0 if is the first interaction after triggering the consecuences
        //else do the standard
        if(currentSentenceID==0)
        {
            if(sections[currentSectionID].isConsecuence)
            {
                if(consecuencesManagerRef.HasTriggeredConsecuence(iReactToConsecuence))
                {
                    currentSentences = DiviveSectionInSentences();
                    if (sections[currentSectionID].triggersNotification)
                    {
                        NotifyConsecuences();
                    }

                    string currentSentenceConsecuence = currentSentences[currentSentenceID];
                    currentSentenceID++;
                    return currentSentenceConsecuence;
                }
                else
                {
                    PrepareForNextSection();
                }
            }
            currentSentences = DiviveSectionInSentences();
            if(sections[currentSectionID].triggersNotification)
            {
                NotifyConsecuences();
            }
        }
        string currentSentence = currentSentences[currentSentenceID];
        currentSentenceID++;
        return currentSentence;
    }
    protected string[] DiviveSectionInSentences()
    {
        string[] tCurrentSentences = new string[sections[currentSectionID].totalSentences];
        int sentenceID = 0;
        string _letters = sections[currentSectionID].sectionText;
        foreach (char _letter in _letters)
        {
            if (_letter == '+')
            {
                sentenceID++;
            }
            else
            {
                tCurrentSentences[sentenceID] += _letter;
            }
        }
        return tCurrentSentences;
    }
    public bool IsEndOfSection()
    {
        bool answer = false;
        if(currentSentenceID== sections[currentSectionID].totalSentences)
        {
            answer = true;
            PrepareForNextSection();
        }
        return answer;
    }

    public void PrepareForNextSection()
    {
        if(currentSectionID<sections.Count-1)
        {
            currentSectionID++;
        }
        currentSentenceID = 0;
    }
    protected void NotifyConsecuences()
    {
        switch (iTriggerConsecuence)
        {
            case Consecuence.Potatos:
                consecuencesManagerRef.NotifyInspectedPotatos();
                break;
            case Consecuence.HouseA:
                consecuencesManagerRef.NotifyBotehredHouseA();
                break;
            case Consecuence.HouseB:
                consecuencesManagerRef.NotifyBotehredHouseB();
                break;
            case Consecuence.HouseC:
                consecuencesManagerRef.NotifyBotehredHouseC();
                break;
            case Consecuence.Thief:
                consecuencesManagerRef.NotifyStealing();
                break;
            case Consecuence.CrowbarPermit:
                consecuencesManagerRef.NotifyPermitedToCrowbar();
                break;
            case Consecuence.CrowbarCollection:
                consecuencesManagerRef.NotifyObtainedCrowbar();
                break;
            case Consecuence.BridgeReady:
                consecuencesManagerRef.GetWasNotifiedOfBridge();
                break;
        }
    }
    public bool AttemptInteraction()
    {
        bool isInteracting=true;
        if(!uiManagerRef.IsItWriting())
        {
            bool isEndOfInteraction = IsEndOfSection();
            uiManagerRef.NotifyInteractionEnd(isEndOfInteraction);
            if(!isEndOfInteraction)
            {
                uiManagerRef.AttemptInteraction(GetCurrentSentence());
                isInteracting = true;
            }
            else
            {
                uiManagerRef.CloseDialogUI();
                isInteracting = false;
            }
        }
        else
        {
            uiManagerRef.PasteAllTextToTextmesh();
            isInteracting = true;
        }
        return isInteracting;
    }
}



[System.Serializable]
public class DialogTextContainer
{
    [Header("------------------------------------------------------------")]

    [TextArea(3, 10)]
    public string sectionText;
    public int totalSentences;
    public bool triggersNotification;
    public bool isConsecuence;
}
