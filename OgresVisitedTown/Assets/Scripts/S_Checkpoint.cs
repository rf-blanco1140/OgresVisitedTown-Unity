using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Checkpoint : MonoBehaviour
{
    [SerializeField] private bool isDefaulPosition;
    private S_PlayerSpawnManager spawnManagerRef;

    private void Start()
    {
        spawnManagerRef = FindObjectOfType<S_PlayerSpawnManager>();
        if(isDefaulPosition)
        {
            spawnManagerRef.DefineCheckpoint(transform.position);
            spawnManagerRef.SetPlayerPosition();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("LOL");
            spawnManagerRef.DefineCheckpoint(transform.position);
        }
    }
}
