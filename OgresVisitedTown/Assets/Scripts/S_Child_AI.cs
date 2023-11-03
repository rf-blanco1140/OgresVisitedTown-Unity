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
    [SerializeField] private float PathRefreshTime=0.5f;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;
    private Seeker seeker;
    private Rigidbody2D rb;
    [SerializeField] private float minDistanceToPlayer = 0.5f;
    private Animator animator;
    

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();

        InvokeRepeating("UpdatePath", 0f, PathRefreshTime);
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
            reachedEndOfPath = false;
        }
        
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        MoveChild(direction);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if(distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }

    private void MoveChild(Vector2 pDirecion)
    {
        bool isMoving = false;
        Vector2 playerPos = new Vector2(target.position.x, target.position.y);
        float distanceToPlayer = Vector2.Distance(rb.position, playerPos);
        if (!reachedEndOfPath && distanceToPlayer>minDistanceToPlayer)
        {
            rb.MovePosition(rb.position + pDirecion * speed * Time.deltaTime);
            isMoving = true;
        }

        SetGfxOrientation(isMoving, pDirecion);
    }

    private void SetGfxOrientation(bool pMoving, Vector2 pDirection)
    {
        float hor = animator.GetFloat("Horizontal");
        float ver = animator.GetFloat("Vertical");

        float xValue = pDirection.x * pDirection.x;
        float yValue = pDirection.y * pDirection.y;
        if (xValue > yValue)
        {
            xValue = 1*Mathf.Sign(pDirection.x);
            yValue = 0;
        }
        else if (xValue < yValue)
        {
            xValue = 0;
            yValue = 1 * Mathf.Sign(pDirection.y);
        }
        else
        {
            xValue = hor;
            yValue = ver;
        }

        animator.SetFloat("Horizontal", xValue);
        animator.SetFloat("Vertical", yValue);
        animator.SetBool("IsMoving", pMoving);
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
