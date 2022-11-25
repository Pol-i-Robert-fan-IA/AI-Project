using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using Pada1.BBCore.Framework; // BasePrimitiveAction
using UnityEngine;

[Action("Vector3/GetClosestKiller")]
public class GetClosestKiller : BasePrimitiveAction
{

    [InParam("GameObject")]
    private GameObject go;

    [InParam("MyEvents")]
    private MyEvents myEvents;

    [OutParam("Killer")]
    public GameObject killer = null;

    public override void OnStart()
    {
    }

    public override TaskStatus OnUpdate()
    {
        if (myEvents.killers.Count == 0) return TaskStatus.RUNNING;

        foreach (var k in myEvents.killers)
        {
            //If there's no skeleton.
            if (killer == null)
            {
                killer = k.gameObject;

                continue;
            }

            //If the skeleton position is smaller than the one already assigned
            if (Vector3.Distance(go.transform.position, k.transform.position) <
                Vector3.Distance(go.transform.position, killer.transform.position))
            {
                killer = k.gameObject;
            }
        }

        if (killer != null) return TaskStatus.COMPLETED;
        else return TaskStatus.RUNNING;
    }

}