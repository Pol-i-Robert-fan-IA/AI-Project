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
        SEEK,
        BACK
    }

    public LEADER_STATE state = LEADER_STATE.FLOCKING;
    Flocking_AI flocking = null;


    [SerializeField] float detectionRadius = 10.0f;
    [SerializeField] Runner_AI[] runners;
    public Runner_AI target;

    //Seek
    private NavMeshAgent agent;

    //Back
    private Transform basePoint;

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
        basePoint = transform.parent.transform;
        flocking.followingTarget = transform.parent.transform;
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
                state = LEADER_STATE.BACK;
                agent.enabled = false;
                flocking.enabled = true;
            }

            if(state == LEADER_STATE.SEEK) Seek();
            //if (state == LEADER_STATE.BACK) GoBackToPoint();
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
        target = null;
        return false;
    }

    private void Seek()
    {
        agent.SetDestination(target.transform.position);
        currentDelay = 0.0f;
        
    }

    //private void GoBackToPoint()
    //{
    //    agent.SetDestination(basePoint.position);
    //    if (Vector3.Distance(transform.position, basePoint.position) < 3.0f)
    //    {
    //        state = LEADER_STATE.FLOCKING;
    //        flocking.enabled = true;
    //    }
    //}
}
