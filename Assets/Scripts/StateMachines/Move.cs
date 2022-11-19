using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Move : MonoBehaviour
{
    [SerializeField] private float wanderRangeMin = 2.0f;
    [SerializeField] private float wanderRangeMax = 10.0f;

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
        Vector3 origin = new Vector3(Random.Range(wanderRangeMin, wanderRangeMax), 100, Random.Range(wanderRangeMin, wanderRangeMax));
        if (Physics.Raycast(origin, transform.TransformDirection(Vector3.down), out hit, 200.0f, layerMask))
        {
            Debug.DrawRay(origin, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
        }

        return (hit.point);
    }

    private void LookForCorpse()
    {

    }
}
