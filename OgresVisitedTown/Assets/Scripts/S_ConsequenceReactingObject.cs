using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_ConsequenceReactingObject : MonoBehaviour
{
    [SerializeField] private GameObject originalObject;
    [SerializeField] private GameObject updatedObject;


    private void Start()
    {
        originalObject.SetActive(true);
        updatedObject.SetActive(false);
    }
    public void ModifyObject()
    {
        originalObject.SetActive(false);
        updatedObject.SetActive(true);
    }
}
