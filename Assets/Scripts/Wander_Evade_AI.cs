using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wander_Evade_AI : MonoBehaviour
{
    [Header("Path")]
    [SerializeField] Node_Path path;
    [Header("Runner")]
    [SerializeField] GameObject runner;
    [Header("Variables")]
    [Range(5.0f, 10.0f)]
    [SerializeField] float evadeArea;
    [Range(2.0f, 5.0f)]
    [SerializeField] float stopArea;
    [Range(2.0f, 8.0f)]
    [SerializeField] float rotationspeed = 3.0f;
    private NavMeshAgent agent;
    [SerializeField] int currentNode = 0;
    Node[] node;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        node = path.GetPathNodes();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 followghost = Vector3.zero;
        Vector3 evasion = Vector3.zero;

        

        if (Vector3.Distance(runner.transform.position, this.transform.position) < evadeArea)
        {
            //Vector From runner to Gameobject that has this script
            evasion = (this.transform.position - runner.transform.position);
            agent.SetDestination(evasion);
            
        }
        else
        {

            if (Vector3.Distance(node[currentNode].transform.position, this.agent.transform.position) < stopArea)
            {
                
                if (currentNode == (path.GetPathNodes().Length - 1))
                {
                    currentNode = 0;
                }
                else
                {
                    currentNode++;
                }
            }
            //Vector From the Gameobject that has this script to Ghost
            agent.SetDestination(node[currentNode].transform.position);
        }

      

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, evadeArea);
        Gizmos.DrawWireSphere(transform.position, stopArea);
    }
}
