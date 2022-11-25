using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Actions
{
    /// <summary>
    /// It is an action to move the GameObject to a given position.
    /// </summary>
    [Action("Navigation/MoveToPosition")]
    [Help("Moves the game object to a given position by using a NavMeshAgent")]
    public class MoveToPosition : GOAction
    {
        ///<value>Input target position where the game object will be moved Parameter.</value>
        [InParam("Spot")]
        [Help("Target position where the game object will be moved")]
        public GameObject spot;

        [InParam("StopRange")]
        public float stopRange = 0;

        private UnityEngine.AI.NavMeshAgent navAgent;

        /// <summary>Initialization Method of MoveToPosition.</summary>
        /// <remarks>Check if there is a NavMeshAgent to assign a default one and assign the destination to the NavMeshAgent the given position.</remarks>
        public override void OnStart()
        {
            navAgent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
            if (navAgent == null)
            {
                Debug.LogWarning("The " + gameObject.name + " game object does not have a Nav Mesh Agent component to navigate. One with default values has been added", gameObject);
                navAgent = gameObject.AddComponent<UnityEngine.AI.NavMeshAgent>();
            }
            
            if (Vector3.Distance(navAgent.transform.position, spot.transform.position) > stopRange)
            {
                navAgent.SetDestination(spot.transform.position);
            }

            #if UNITY_5_6_OR_NEWER
                navAgent.isStopped = false;
            #else
                navAgent.Resume();
            #endif
        }

        /// <summary>Method of Update of MoveToPosition </summary>
        /// <remarks>Check the status of the task, if it has traveled the road or is close to the goal it is completed
        /// and otherwise it will remain in operation.</remarks>
        public override TaskStatus OnUpdate()
        {
            if (!navAgent.pathPending && navAgent.remainingDistance <= stopRange)
                return TaskStatus.COMPLETED;

            return TaskStatus.RUNNING;
        }

        /// <summary>Abort method of MoveToPosition.</summary>
        /// <remarks>When the task is aborted, it stops the navAgentMesh.</remarks>
        public override void OnAbort()
        {
#if UNITY_5_6_OR_NEWER
            if(navAgent!=null)
                navAgent.isStopped = true;
#else
            if (navAgent != null)
                navAgent.Stop();
            #endif
        }
    }
}
