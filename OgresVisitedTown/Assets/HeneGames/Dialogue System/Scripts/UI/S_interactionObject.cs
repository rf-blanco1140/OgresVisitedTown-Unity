using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class S_interactionObject : MonoBehaviour
{
    private int currentSectionID;
    private int currentSentenceID;
    private string[] currentSentences;
        
    [Header("Dialogue")]
    [SerializeField] private List<DialogTextContainer> sections = new List<DialogTextContainer>();

    private void Start()
    {
        currentSectionID = 0;
        currentSentenceID = 0;
    }
    public string GetCurrentSentence()
    {
        if(currentSentenceID==0)
        {
            currentSentences = DiviveSectionInSentences();
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
        Debug.Log("current Section ID: "+currentSectionID);
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
}



[System.Serializable]
public class DialogTextContainer
{
    [Header("------------------------------------------------------------")]

    [TextArea(3, 10)]
    public string sectionText;
    public int totalSentences;
}
