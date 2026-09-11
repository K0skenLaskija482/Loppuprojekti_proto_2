using UnityEngine;
using System.Collections.Generic;

public class WaypointManager : MonoBehaviour
{
    public List<Transform> waypoints = new List<Transform>();
    public bool isMoving;
    public int waypointIndex;
    [SerializeField] float movespeed = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartMoving();
    }

    public void StartMoving()
    {
        waypointIndex = 0;
        isMoving = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving) { return; }

        transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].position, Time.deltaTime*movespeed);
    }
}
