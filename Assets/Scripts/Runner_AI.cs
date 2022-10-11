using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Runner_AI : MonoBehaviour
{
    [SerializeField] float sttopingRange = 2.0f;

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
        if (CheckNode())
        {
            if (currentNode == (path.GetPathNodes().Length-1)) currentNode = 0;
            else currentNode++;
            agent.SetDestination(GetNodePos());
        }

    }

    bool CheckNode()
    {
        if (Vector3.Distance(GetNodePos(), transform.position) < sttopingRange)
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