using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using Pada1.BBCore.Framework; // BasePrimitiveAction
using UnityEngine;



[Action("Vector3/MoveToWounded")]
[Help("Gets a vector to wounded position")]
public class MoveToWoundedGameObject : BasePrimitiveAction
{
    [InParam("Healer")]
    public GameObject Healer;

    private UnityEngine.AI.NavMeshAgent navAgent;

    private GameObject Wounded;

    [InParam("Stop Distance")]
    public float stopdistance;

    public override void OnStart()
    {
      
        navAgent = Healer.GetComponent<UnityEngine.AI.NavMeshAgent>();
        navAgent.isStopped = false;
        Wounded = GameObject.FindGameObjectWithTag("Wounded");
        if (Vector3.Distance(Wounded.transform.position, Healer.transform.position) > stopdistance)
        {
            Debug.Log("Distancia = " + Vector3.Distance(Wounded.transform.position, Healer.transform.position));
            Debug.Log("Stopdistance = " + stopdistance);
            navAgent.SetDestination(Wounded.transform.position);
        }
    }
    public override TaskStatus OnUpdate()
    {

        if(Vector3.Distance(Wounded.transform.position, Healer.transform.position) < stopdistance)
        {
            navAgent.SetDestination(Healer.transform.position);
            return TaskStatus.COMPLETED;
        }


        navAgent.SetDestination(Wounded.transform.position);
        
        return TaskStatus.RUNNING;
    }
}
