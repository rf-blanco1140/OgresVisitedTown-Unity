using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_QuestionObject : S_interactionObject
{
    private S_uiManager uiManagerRef;

    public override string GetCurrentSentence()
    {
        string rtnText;
        uiManagerRef.ShowHideChoiceUI(true);
        rtnText = base.GetCurrentSentence();
        return rtnText;
    }
}
