using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class S_interactionObject : MonoBehaviour
{
    private int currentSectionID;
    private int currentSentenceID;
    private string[] currentSentences;
    [SerializeField] private S_interactionConsecuencesManager consecuencesManagerRef;
    private enum Consecuence { None, Potatos, HouseA, HouseB, HouseC, Thief, BridgeReady}
    [SerializeField] private Consecuence consecuence;
    [SerializeField] private bool hasExclusiveConsecuence;    
    [Header("Dialogue")]
    [SerializeField] private List<DialogTextContainer> sections = new List<DialogTextContainer>();

    private void Start()
    {
        currentSectionID = 0;
        currentSentenceID = 0;
        consecuencesManagerRef = FindObjectOfType<S_interactionConsecuencesManager>();
    }
    public string GetCurrentSentence()
    {
        if(currentSentenceID==0)
        {
            currentSentences = DiviveSectionInSentences();
            if(sections[currentSectionID].triggerNotification)
            {
                NotifyConsecuences();
            }
        }
        string currentSentence = currentSentences[currentSentenceID];
        currentSentenceID++;
        return currentSentence;
    }
    private string[] DiviveSectionInSentences()
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
    private void NotifyConsecuences()
    {
        switch (consecuence)
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
            case Consecuence.BridgeReady:
                consecuencesManagerRef.GetWasNotifiedOfBridge();
                break;
        }
    }
}



[System.Serializable]
public class DialogTextContainer
{
    [Header("------------------------------------------------------------")]

    [TextArea(3, 10)]
    public string sectionText;
    public int totalSentences;
    public bool triggerNotification;
    public bool isConsecuence;
}
