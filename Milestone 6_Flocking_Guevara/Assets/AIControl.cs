using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIControl : MonoBehaviour
{
    GameObject[] goalLocations;
    NavMeshAgent agent;
    Animator anim;
    float speedMultiplier;
    float detectionRadius = 5;
    float fleeRadius = 10;

    private void ResetAgent()
    {
        speedMultiplier = Random.Range(0.1f, 1f);
        agent.speed = 2 * speedMultiplier;
        agent.angularSpeed = 120;
        anim.SetFloat("speedMultiplier", speedMultiplier);
        anim.SetTrigger("isWalking");
        agent.ResetPath();
    }

    // Start is called before the first frame update
    void Start()
    {
        goalLocations = GameObject.FindGameObjectsWithTag("goal");
        agent = this.GetComponent<NavMeshAgent>();
        anim = this.GetComponent<Animator>();

        agent.SetDestination(goalLocations[Random.Range(0, goalLocations.Length)].transform.position);
        anim.SetFloat("wOffset", Random.Range(0.1f, 1f));

        ResetAgent();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(agent.remainingDistance < 1)
        {
            Debug.Log("Working!");
            ResetAgent();
            agent.SetDestination(goalLocations[Random.Range(0, goalLocations.Length)].transform.position);
        }
    }

    public void DetectNewObstacle(Vector3 location)
    {
        if(Vector3.Distance(location,this.transform.position) < detectionRadius)
        {
            Vector3 fleeDirection = (this.transform.position - location).normalized;
            Vector3 newGoal = this.transform.position + fleeDirection * fleeRadius;

            NavMeshPath path = new NavMeshPath();
            agent.CalculatePath(newGoal, path);

            if(path.status != NavMeshPathStatus.PathInvalid)
            {
                agent.SetDestination(path.corners[path.corners.Length - 1]);
                anim.SetTrigger("isRunning");
                agent.speed = 10;
                agent.angularSpeed = 500;
            }
        }
    }

    public void DetectNewAttraction(Vector3 location)
    {
        if (Vector3.Distance(location, this.transform.position) < detectionRadius)
        {
            NavMeshPath path = new NavMeshPath();
            agent.CalculatePath(location, path);

            if (path.status != NavMeshPathStatus.PathInvalid)
            {
                agent.SetDestination(path.corners[path.corners.Length - 1]);
                anim.SetTrigger("isRunning");
                agent.speed = 10;
                agent.angularSpeed = 500;
            }
        }
    }
}
