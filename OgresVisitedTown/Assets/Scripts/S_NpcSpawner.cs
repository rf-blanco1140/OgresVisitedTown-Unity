using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_NpcSpawner : MonoBehaviour
{
    [SerializeField] GameObject npc;

    public void SpawnNPC()
    {
        npc.SetActive(true);
    }
}
