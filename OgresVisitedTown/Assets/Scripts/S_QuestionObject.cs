using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_QuestionObject : S_interactionObject
{

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
                        Debug.Log("Show options UI");
                        uiManagerRef.ShowHideChoiceUI(true);

                    }
                    else
                    {
                        uiManagerRef.CloseDialogUI();
                        isInteracting = false;
                    }
                }
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
