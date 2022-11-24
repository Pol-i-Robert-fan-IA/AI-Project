using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using Pada1.BBCore.Framework; // BasePrimitiveAction
using UnityEngine;



[Action("Vector3/MoveToSkeleton")]
[Help("Gets a vector to wounded position")]
public class MoveToWoundedGameObject : BasePrimitiveAction
{
    [InParam("Healer")]
    public GameObject Healer;

    private UnityEngine.AI.NavMeshAgent navAgent;

    [InParam("Skeleton")]
    public GameObject Skeleton;

    [InParam("Stop Distance")]
    public float stopdistance;

    public override void OnStart()
    {

        navAgent = Healer.GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (Vector3.Distance(Skeleton.transform.position, Healer.transform.position) > stopdistance)
        {
            Debug.Log("Distancia = " + Vector3.Distance(Skeleton.transform.position, Healer.transform.position));
            Debug.Log("Stopdistance = " + stopdistance);
            navAgent.SetDestination(Skeleton.transform.position);
        }
    }
    public override TaskStatus OnUpdate()
    {

        if (Vector3.Distance(Skeleton.transform.position, Healer.transform.position) < stopdistance)
        {

            return TaskStatus.COMPLETED;
        }


        navAgent.SetDestination(Skeleton.transform.position);

        return TaskStatus.RUNNING;
    }
}