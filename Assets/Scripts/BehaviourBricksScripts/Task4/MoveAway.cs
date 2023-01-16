using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using Pada1.BBCore.Framework; // BasePrimitiveAction
using UnityEngine;
using Unity.VisualScripting;
using Skeleton;


[Action("Vector3/MoveAway")]
public class MoveAway : BasePrimitiveAction
{

    [InParam("Self")]
    private GameObject self;

    [InParam("Target")]
    private GameObject target;

    [InParam("MultiplyBy")]
    private float multiplyBy;

    private Transform startTransform;

    private UnityEngine.AI.NavMeshAgent navAgent;
    
    public override void OnStart()
    {
        startTransform = self.transform;
        navAgent = self.GetComponent<UnityEngine.AI.NavMeshAgent>();
        Transform _transform = self.transform;
        //temporarily point the object to look away from the player
        _transform.rotation = Quaternion.LookRotation(self.transform.position - target.transform.position);

        //Then we'll get the position on that rotation that's multiplyBy down the path (you could set a Random.range
        // for t$$anonymous$$s if you want variable results) and store it in a new Vector3 called runTo
        Vector3 runTo = _transform.position + _transform.forward * multiplyBy;

        UnityEngine.AI.NavMeshHit meshHit;

        // 5 is the distance to check, assumes you use default for the NavMesh Layer name
        if (!UnityEngine.AI.NavMesh.SamplePosition(runTo, out meshHit, 5, 1 << UnityEngine.AI.NavMesh.GetNavMeshLayerFromName("Default")))
        {
            Debug.Log("First Mesh Hit Failed");
            _transform.rotation = Quaternion.AngleAxis(100.0f, Vector3.up);
            runTo = _transform.position + _transform.forward * multiplyBy;
            if (!UnityEngine.AI.NavMesh.SamplePosition(runTo, out meshHit, 5, 1 << UnityEngine.AI.NavMesh.GetNavMeshLayerFromName("Default")))
            {
                Debug.Log("Second Mesh Hit Failed");
                _transform.rotation = Quaternion.AngleAxis(-200.0f, Vector3.up);
                runTo = _transform.position + _transform.forward * multiplyBy;
                UnityEngine.AI.NavMesh.SamplePosition(runTo, out meshHit, 5, 1 << UnityEngine.AI.NavMesh.GetNavMeshLayerFromName("Default"));
            }
        }

        //Debug.Log("$$anonymous$$t = " + $$anonymous$$t + " $$anonymous$$t.position = " + $$anonymous$$t.position);
        
        // just used for testing - safe to ignore
        //nextTurnTime = Time.time + 5;

        // reset the transform back to our start transform
        self.transform.position = startTransform.position;
        self.transform.rotation = startTransform.rotation;

        // And get it to head towards the found NavMesh position
        navAgent.SetDestination(meshHit.position);
    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.COMPLETED;
    }
}
