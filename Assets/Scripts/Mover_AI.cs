using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover_AI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float maxVelocity = 10.0f;
    [SerializeField] float acceleration = 2.0f;
    float velocity = 0.0f;

    [SerializeField] float maxTurnSpeed = 8.0f;
    [SerializeField] float turnAcceleration = 1.0f;
    float turnSpeed = 0.0f;

    [SerializeField] float sttopingRange = 6.0f;

    [SerializeField] Node_Path path;
    private int currentNode = 0;

    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.SetDestination(GetNodePos());
    }

    
    void Update()
    {
        if(CheckNode())
        {
            if (currentNode == path.GetPathNodes().Length) currentNode = 0;
            else currentNode++;
            agent.SetDestination(GetNodePos());
        }
        
    }

    bool CheckNode()
    {
        if(Vector3.Distance(target.transform.position, transform.position) < sttopingRange)
        {
            return true;
        }
        return false;
    }

    Vector3 GetNodePos()
    {
        Node[] nodes = path.GetPathNodes();
        return nodes[currentNode].transform.position;
    }
}
