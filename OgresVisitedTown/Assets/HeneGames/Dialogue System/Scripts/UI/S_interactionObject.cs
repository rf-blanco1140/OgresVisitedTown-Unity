using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class S_interactionObject : MonoBehaviour
{
    private int currentSectionID;
    private int currentSentenceID;
    private string[] currentSentences;
        
    [Header("Dialogue")]
    [SerializeField] private List<DialogTextContainer> sentences = new List<DialogTextContainer>();

    public int GetCurrentDialogSectionID()
    {
        return currentSectionID;
    }
    public void IncreasecurrentDialogSectionID()
    {
        currentSectionID++;
    }
    public string GetCurrentSentence()
    {
        if(currentSentenceID==0)
        {
            currentSentences = DiviveSectionInSnetences();
        }
        string currentSentence = currentSentences[currentSentenceID];
        currentSentenceID++;
        return currentSentence;
    }
    private string[] DiviveSectionInSnetences()
    {
        string[] tCurrentSentences = new string[sentences[currentSectionID].totalSentences];
        int sentenceID = 0;
        string _letters = sentences[currentSectionID].sectionText;
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
        if(currentSentenceID== sentences[currentSectionID].totalSentences)
        {
            answer = true;
        }
        return answer;
    }

    public void PrepareForNextSection()
    {
        currentSentenceID = 0;
        currentSectionID = 0;
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
