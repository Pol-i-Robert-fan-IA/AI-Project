using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wander_Evade_AI : MonoBehaviour
{
    [Header("Path")]
    [SerializeField] Node_Path path;

    [Header("Variables")]
    [Range(5.0f, 10.0f)][SerializeField] float evadeArea;
    [Range(2.0f, 5.0f)][SerializeField] float stopArea;

    //Nodes
    [SerializeField] int currentNode = 0;
    Node[] node;

    GameObject[] runnerArray;
    GameObject runnertoavoid;

    private NavMeshAgent agent;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        runnerArray = GameObject.FindGameObjectsWithTag("Runner");

        agent = GetComponent<NavMeshAgent>();

        node = path.GetPathNodes();
        
        animator = GetComponentInChildren<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 followghost = Vector3.zero;
        Vector3 evasion = Vector3.zero;
       

        if (DistanceRunners())
        {
            //Vector From runner to Gameobject that has this script
            evasion = (this.transform.position - runnertoavoid.transform.position);
            agent.SetDestination(evasion + this.transform.position);

            agent.acceleration = 8.0f;
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

            agent.acceleration = 4.0f;

        }

    }

    bool DistanceRunners()
    {
        for(int i = 0; i <runnerArray.Length;i++)
        {
            if(Vector3.Distance(runnerArray[i].transform.position, this.transform.position) < evadeArea)
            {
                runnertoavoid = runnerArray[i];
                animator.Play("Running_Away");
                return true;
            }
        }

        animator.Play("Running_Normal");
        return false;
    }

}
