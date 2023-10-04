using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PlayerSpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject playerRef;

    private void Start()
    {
        playerRef = FindObjectOfType<S_playerInteractionManager>().gameObject;
        playerRef.transform.position = transform.position;
    }
}
