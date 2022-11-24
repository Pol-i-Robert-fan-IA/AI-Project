using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using Pada1.BBCore.Framework; // BasePrimitiveAction
using UnityEngine;
using Unity.VisualScripting;
using Skeleton;


[Action("Vector3/RessSkeleton")]
public class RessSkeleton : BasePrimitiveAction
{
    [InParam("MyEvents")]
    private MyEvents myEvents;

    [InParam("Skeleton")]
    [OutParam("Skeleton")]
    private GameObject skeleton;

    [InParam("SkeletonId")]
    private int skeletonId;

    public override void OnStart()
    {
        skeleton.GetComponent<Blackboard_Skeleton>().isDead = false;
        myEvents.deathSkeletons.RemoveAt(skeletonId);
        skeletonId = -1;
        skeleton = null;
    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.COMPLETED;
    }
}
