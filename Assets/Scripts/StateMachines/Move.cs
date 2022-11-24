using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Move : MonoBehaviour
{

    [SerializeField] private float wanderRange = 15.0f;

    [SerializeField] private LayerMask layerMask = 0;

    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StopAgent()
    {
        agent.isStopped = true;
    }

    public void StartAgent()
    {
        agent.isStopped = false;
    }

    public void Displace(Vector3 point)
    {
        agent.SetDestination(point);
    }

    public Vector3 NewPoint()
    {
        Vector3 point = RandomRayCast();
        agent.SetDestination(point);

        return point;
    }

    public void Wander()
    {

    }

    public void Action()
    {

    }

    private Vector3 RandomRayCast()
    {
        RaycastHit hit;
        Vector3 origin = transform.position + Random.insideUnitSphere * wanderRange;

        origin.y = 100.0f;
        if (Physics.Raycast(origin, transform.TransformDirection(Vector3.down), out hit, 200.0f, layerMask))
        {
            Debug.DrawRay(origin, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
        }

        return (hit.point);
    }
}
