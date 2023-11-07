using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_QuestionObject : S_interactionObject
{
    [SerializeField] private S_NpcSpawner spawner;

    public override bool AttemptInteraction()
    {
        int thisLoopSectionID = currentSectionID;
        bool isInteracting = true;
        if(!uiManagerRef.IsItWriting())
        {
            if(!uiManagerRef.isChoiceUiActive())
            {
                bool isEndOfInteraction = IsEndOfSection();
                uiManagerRef.NotifyInteractionEnd(isEndOfInteraction);
                if (!isEndOfInteraction)
                {
                    uiManagerRef.AttemptInteraction(GetCurrentSentence());
                    isInteracting = true;
                }
                else
                {
                    if (thisLoopSectionID == sections.Count-1)
                    {
                        uiManagerRef.ShowHideChoiceUI(true);

                    }
                    else
                    {
                        uiManagerRef.CloseDialogUI();
                        isInteracting = false;
                    }
                }
            }
            else
            {
                Debug.Log("try closing");
                isInteracting = false;
                uiManagerRef.ShowHideChoiceUI(false);
                uiManagerRef.CloseDialogUI();
            }
        }
        else
        {
            uiManagerRef.PasteAllTextToTextmesh();
            isInteracting = true;
        }

        return isInteracting;
    }

    public void ExecuteAnswer(bool pAnswer)
    {
        if (pAnswer)
        {
            spawner.SpawnNPC();
        }
        //uiManagerRef.ShowHideChoiceUI(false);
        //uiManagerRef.CloseDialogUI();
    }
}
