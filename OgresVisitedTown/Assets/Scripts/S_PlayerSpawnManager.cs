using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PlayerSpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject playerRef;
    private Vector2 currentCheckpoint;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        playerRef = FindObjectOfType<S_playerInteractionManager>().gameObject;
    }

    public void DefineCheckpoint(Vector2 pNewPosition)
    {
        currentCheckpoint = pNewPosition;
    }

    public void SetPlayerPosition()
    {
        playerRef.transform.position = currentCheckpoint;
    }
}
