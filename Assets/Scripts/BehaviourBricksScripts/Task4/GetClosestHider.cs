using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using Pada1.BBCore.Framework; // BasePrimitiveAction
using UnityEngine;

[Action("Vector3/GetClosestHider")]
public class GetClosestHider : BasePrimitiveAction
{

    [InParam("GameObject")]
    private GameObject go;

    [InParam("MyEvents")]
    private MyEvents myEvents;

    [InParam("Hider")]
    [OutParam("Hider")]
    public GameObject hider = null;

    public override void OnStart()
    {
    }

    public override TaskStatus OnUpdate()
    {
        if (myEvents.hiders.Count == 0) return TaskStatus.RUNNING;

        foreach (var k in myEvents.hiders)
        {
            //If there's no skeleton.
            if (hider == null)
            {
                hider = k.gameObject;

                continue;
            }

            //If the skeleton position is smaller than the one already assigned
            if (Vector3.Distance(go.transform.position, k.transform.position) <
                Vector3.Distance(go.transform.position, hider.transform.position))
            {
                hider = k.gameObject;
            }
        }

        if (hider != null) return TaskStatus.COMPLETED;
        else return TaskStatus.RUNNING;
    }

}