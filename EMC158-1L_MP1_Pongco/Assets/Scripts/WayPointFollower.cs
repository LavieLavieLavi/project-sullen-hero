using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointFollower : MonoBehaviour
{
    //Set an array for to contain the waypoint
    [SerializeField] GameObject[] wayPoints;
    int currentWaypointIndex = 0;

    [SerializeField] float platformSpeed = 1f;

    // Update is called once per frame
    void Update()
    {
        // Assuming waypoints is an array of Transform objects
        if (Vector3.Distance(transform.position, wayPoints[currentWaypointIndex].transform.transform.transform.position) < .1f) // this condition check if the platform is close to the if true then next index
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= wayPoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, wayPoints[currentWaypointIndex].transform.position, platformSpeed * Time.deltaTime);



    }
}
