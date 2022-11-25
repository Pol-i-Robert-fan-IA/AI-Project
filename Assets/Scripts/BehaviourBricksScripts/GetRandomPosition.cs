using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using Pada1.BBCore.Framework; // BasePrimitiveAction
using UnityEngine;



[Action("Vector3/Move&GetRandomPosition")]
[Help("Gets a random position")]
public class GetRandomPosition : BasePrimitiveAction
{
    [InParam("GameObject")]
    public GameObject go;

    private UnityEngine.AI.NavMeshAgent navAgent;

    private Move move;

    public override void OnStart()
    {
        navAgent = go.GetComponent<UnityEngine.AI.NavMeshAgent>();
        move = go.GetComponent<Move>();
        move.NewPoint();
    }
    public override TaskStatus OnUpdate()
    {
        if (!navAgent.pathPending && navAgent.remainingDistance <= navAgent.stoppingDistance)
        {
            return TaskStatus.COMPLETED;
        }
            return TaskStatus.RUNNING;
    }
}

