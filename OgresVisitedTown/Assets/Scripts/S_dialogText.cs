using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_dialogText : MonoBehaviour
{
    [Header("Dialogue")]
    [SerializeField] private List<DialogTextContainer> sections = new List<DialogTextContainer>();
    public List<DialogTextContainer> GetOptionalSections()
    {
        return sections;
    }
}
