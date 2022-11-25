using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using Pada1.BBCore.Framework; // BasePrimitiveAction
using UnityEngine;
using Unity.VisualScripting;
using Skeleton;


[Action("Vector3/KillSkeleton")]
public class KillSkeleton : BasePrimitiveAction
{
    [InParam("MyEvents")]
    private MyEvents myEvents;

    [InParam("Self")]
    private GameObject self;

    [InParam("Skeleton")]
    [OutParam("Skeleton")]
    private GameObject skeleton;

    [InParam("SkeletonId")]
    private int skeletonId;

    public override void OnStart()
    {
        if (skeleton != null)
        {
            skeleton.GetComponent<Blackboard_Skeleton>().isDead = true;

                myEvents.aliveSkeletons.RemoveAt(skeletonId);
                skeletonId = -1;
                skeleton = null;
            //Adds the Killer to the list of killers so the Golem starts chasing them
            Killer killer = self.GetComponent<Killer>();

                myEvents.killers.Add(killer);
        }
    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.COMPLETED;
    }
}
