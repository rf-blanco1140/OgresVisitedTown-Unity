using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Enemy_Patrol_Movement : MonoBehaviour
{
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField]private int nextPoint;
    private Rigidbody2D rb;
    [SerializeField] private float speed;
    private Vector2 direction;
    private Vector2 distanceToNextPoint;

    private void Start()
    {
        nextPoint = 0;
        rb = gameObject.GetComponent<Rigidbody2D>();
        direction = new Vector2();
        distanceToNextPoint = new Vector2();
    }

    private void Update()
    {
        distanceToNextPoint = ((Vector2)patrolPoints[nextPoint].position - (Vector2)rb.transform.position);
        float xDistance = Mathf.Abs(distanceToNextPoint.x);
        float yDistance = Mathf.Abs(distanceToNextPoint.y);
        if (xDistance < 0.1 && yDistance < 0.1)
        {
            if(nextPoint == patrolPoints.Length-1)
            {
                nextPoint = 0;
            }
            else
            {
                nextPoint++;
            }
            direction = ((Vector2)patrolPoints[nextPoint].position- (Vector2)rb.transform.position ).normalized;
        }

        MoveToNextPoint();
    }

    private void MoveToNextPoint()
    {
        rb.MovePosition(rb.position + direction * speed * Time.deltaTime);
    }
}
