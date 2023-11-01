using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class S_Child_AI : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed = 200f;
    [SerializeField] private float nextWaypointDistance = 3f;
    private Path path;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;
    private Seeker seeker;
    private Rigidbody2D rb;
    [SerializeField] private float minDistanceToPlayer = 0.5f;
    

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, .15f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       if(path==null)
        {
            return;
        }
       if(currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
       else
        {
            //Debug.Log("current waypoint: " + currentWaypoint);
            //Debug.Log("Total Waypoints: " + path.vectorPath.Count);
            reachedEndOfPath = false;
        }
        
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        //Vector2 force = direction * speed * Time.deltaTime;
        //rb.AddForce(force);
        MoveChild(direction);
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if(distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }

    private void MoveChild(Vector2 pDirecion)
    {
        Vector2 playerPos = new Vector2(target.position.x, target.position.y);
        float distanceToPlayer = Vector2.Distance(rb.position, playerPos);
        if (!reachedEndOfPath && distanceToPlayer>minDistanceToPlayer)
        {
            rb.MovePosition(rb.position + pDirecion * speed * Time.deltaTime);
        }

    }

    private void OnPathComplete(Path pPath)
    {
        if(!pPath.error)
        {
            path = pPath;
            currentWaypoint = 0;
        }
    }

    private void UpdatePath()
    {
        if(seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }
}
