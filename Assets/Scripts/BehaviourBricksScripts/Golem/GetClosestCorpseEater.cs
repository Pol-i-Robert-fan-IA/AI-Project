using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using Pada1.BBCore.Framework; // BasePrimitiveAction
using UnityEngine;
using Unity.VisualScripting;
using Skeleton;

[Action("Vector3/GetClosesCorpseEater")]
public class GetClosestCorpseEater : BasePrimitiveAction
{

    [InParam("GameObject")]
    private GameObject go;

    [InParam("MyEvents")]
    private MyEvents myEvents;

    [OutParam("Skeleton")]
    public GameObject skeleton = null;

    public override void OnStart()
    {

        

    }

    public override TaskStatus OnUpdate()
    {
        if (myEvents.skeletons.Count == 0) return TaskStatus.RUNNING;

        foreach (var k in myEvents.skeletons)
        {

            //If there's no skeleton.
            if (skeleton == null)
            {
                skeleton = k.gameObject;
                continue;
            }

            //If the skeleton position is smaller than the one already assigned
            if (Vector3.Distance(go.transform.position, k.transform.position) <
                Vector3.Distance(go.transform.position, skeleton.transform.position))
            {
                skeleton = k.gameObject;
            }
        }

        if (skeleton != null) return TaskStatus.COMPLETED;
        else return TaskStatus.RUNNING;
    }

}
