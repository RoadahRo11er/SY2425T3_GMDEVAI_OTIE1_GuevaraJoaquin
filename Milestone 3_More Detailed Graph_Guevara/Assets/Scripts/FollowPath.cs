using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    private Transform goal;
    private float speed = 5.0f;
    private float accuracy = 1.0f;
    private float rotSpeed = 2.0f;

    public GameObject wpManager;
    private GameObject[] wps;
    private GameObject currentNode;
    private int currentWaypointIndex = 0;
    private Graph graph;

    // Start is called before the first frame update
    void Start()
    {
        wps = wpManager.GetComponent<WaypointManager>().waypoints;
        graph = wpManager.GetComponent<WaypointManager>().graph;
        currentNode = wps[0];
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(graph.getPathLength() == 0 || currentWaypointIndex == graph.getPathLength())
        {
            return;
        }

        currentNode = graph.getPathPoint(currentWaypointIndex);

        if(Vector3.Distance(graph.getPathPoint(currentWaypointIndex).transform.position, transform.position) < accuracy)
        {
            currentWaypointIndex++;
        }

        if(currentWaypointIndex < graph.getPathLength())
        {
            goal = graph.getPathPoint(currentWaypointIndex).transform;
            Vector3 lookAtGoal = new Vector3(goal.position.x,transform.position.y,goal.position.z);
            Vector3 direction = lookAtGoal - this.transform.position;

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                                       Quaternion.LookRotation(direction),
                                                       Time.deltaTime * rotSpeed);
            this.transform.Translate(0,0,speed * Time.deltaTime);
        }
    }

    public void GoToHelipad()
    {
        graph.AStar(currentNode, wps[0]);
        currentWaypointIndex = 0;
    }

    public void GoToRuins()
    {
        graph.AStar(currentNode, wps[6]);
        currentWaypointIndex = 0;
    }

    public void GoToFactory()
    {
        graph.AStar(currentNode, wps[7]);
        currentWaypointIndex = 0;
    }

    public void GoToPlace(int num)
    {
        graph.AStar(currentNode, wps[num]);
        currentWaypointIndex = 0;
    }
}
