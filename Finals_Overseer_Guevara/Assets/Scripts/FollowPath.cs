using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    private Transform goal;
    public float speed = 5.0f;
    private float accuracy = 1.0f;
    private float rotSpeed = 2.0f;

    public GameObject wpManager;
    public Link[] waypointLinks;
    private GameObject[] wps;
    private GameObject currentNode;
    private int currentWaypointIndex = 0;
    private int previousNum = 100;
    private GameObject selectedWaypoint;
    private Graph graph;

    // Start is called before the first frame update
    void Start()
    {
        wps = wpManager.GetComponent<WaypointManager>().waypoints;
        graph = wpManager.GetComponent<WaypointManager>().graph;
        waypointLinks = wpManager.GetComponent<WaypointManager>().links;
        currentNode = wps[0];
        selectedWaypoint = currentNode;
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

    public void GoToPlace(int num)
    {
        if(num == previousNum)
        {
            return;
        }
        previousNum = num;
        selectedWaypoint = wps[num];
        graph.AStar(currentNode, wps[num]);
        currentWaypointIndex = 0;
    }

    public void GotoCloseArea()
    {
        List<GameObject> possibleWp = new List<GameObject>();
        foreach(Link link in waypointLinks)
        {
            if(selectedWaypoint == link.node1)
            {
                possibleWp.Add(link.node2);
                Debug.Log(link.node2);
            }else if(selectedWaypoint == link.node2)
            {
                possibleWp.Add(link.node1);
                Debug.Log(link.node1);
            }
        }
        selectedWaypoint = possibleWp[Random.Range(0,possibleWp.Count-1)];
        previousNum = 100;
        graph.AStar(currentNode,selectedWaypoint);
        currentWaypointIndex = 0;
    }
}
