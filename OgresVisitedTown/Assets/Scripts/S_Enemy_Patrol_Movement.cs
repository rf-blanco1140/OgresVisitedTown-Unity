using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Enemy_Patrol_Movement : MonoBehaviour
{
    [SerializeField] private Transform[] patrolPoints;
    private int nextPoint;
    private Rigidbody2D rb;

    private void Start()
    {
        nextPoint = 1;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(rb.transform.position == patrolPoints[nextPoint].position)
        {
            nextPoint++;
        }
        MoveToNextPoint();
    }

    private void MoveToNextPoint()
    {

    }
}
