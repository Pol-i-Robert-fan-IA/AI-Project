using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using Pada1.BBCore.Framework; // BasePrimitiveAction
using UnityEngine;



[Action("Vector3/Move&GetRandomPosition")]
[Help("Gets a random position")]
public class GetRandomPosition : BasePrimitiveAction
{
    [InParam("Range")]
    [Range(1,20)]public float range;
    [InParam("GameObject")]
    public GameObject Healer;

    private UnityEngine.AI.NavMeshAgent navAgent;

    private Vector3 randomPosition;

    public override void OnStart()
    {
        navAgent = Healer.GetComponent<UnityEngine.AI.NavMeshAgent>();
        randomPosition = Random.insideUnitSphere * range;
        randomPosition = randomPosition + Healer.transform.position;

        Debug.Log(randomPosition);
        navAgent.SetDestination(randomPosition);
    }
    public override TaskStatus OnUpdate()
    {
        
        if (!navAgent.pathPending && navAgent.remainingDistance <= navAgent.stoppingDistance)
        {
            return TaskStatus.COMPLETED;
        }
        else
        {
            return TaskStatus.RUNNING;
        }
    }
}

