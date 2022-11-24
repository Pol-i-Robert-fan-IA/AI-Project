using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using Pada1.BBCore.Framework; // BasePrimitiveAction
using UnityEngine;
using Unity.VisualScripting;

[Action("Vector3/bbWander")]
public class bbWander : BasePrimitiveAction
{

    private UnityEngine.AI.NavMeshAgent navAgent;

    [InParam("GameObject")]
    private GameObject go;

    [InParam("Skeleton")]
    private GameObject skeleton;

    private Move move;

    public override void OnStart()
    {
        navAgent = go.GetComponent<UnityEngine.AI.NavMeshAgent>();
        move = go.GetComponent<Move>();
        move.NewPoint();
    }

    public override TaskStatus OnUpdate()
    {
        if (skeleton != null) return TaskStatus.COMPLETED;

        if (!navAgent.pathPending && navAgent.remainingDistance <= navAgent.stoppingDistance)
        {
                return TaskStatus.FAILED;

        }

        return TaskStatus.RUNNING;
    }

}
