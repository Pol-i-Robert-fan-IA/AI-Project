using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Flocking_AI))]
public class Flocking_Leader_AI : MonoBehaviour
{
    public enum LEADER_STATE
    {
        FLOCKING = 0,
        SEEK
    }

    public LEADER_STATE state = LEADER_STATE.FLOCKING;
    Flocking_AI flocking = null;


    [SerializeField] float detectionRadius = 10.0f;
    [SerializeField] Runner_AI[] runners;
    public Runner_AI target;

    //Seek
    private NavMeshAgent agent;

    private float currentDelay = Mathf.Infinity;
    [Tooltip("Update delay")] public float delay = 0.5f;

    private void Awake()
    {
        
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        flocking = GetComponent<Flocking_AI>();
        runners = GameObject.FindObjectsOfType<Runner_AI>();
    }

    void Update()
    {
        if (currentDelay > delay)
        {
            if (CheckRunnersProximity())
            {
                state = LEADER_STATE.SEEK;
                flocking.enabled = false;
                agent.enabled = true;
                Debug.Log(transform.name);
            }
            else
            {
                state = LEADER_STATE.FLOCKING;
                flocking.enabled = true;
                agent.enabled = false;
            }

            if(state == LEADER_STATE.SEEK) Seek();
        }
        currentDelay += Time.deltaTime;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    private bool CheckRunnersProximity()
    {
        for (int i = 0; i < runners.Length; i++)
        {
            if (Vector3.Distance(transform.position, runners[i].transform.position) < detectionRadius)
            {
                target = runners[i];
                return true;
            }
        }

        //if(target != null)
        //{
        //    target = null;
        //    currentDelay = Mathf.Infinity;
        //}

        return false;
    }

    private void Seek()
    {
        agent.SetDestination(target.transform.position);
        currentDelay = 0.0f;
        
    }
}
