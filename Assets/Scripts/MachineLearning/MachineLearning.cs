//using System.Collections.Generic;
//using UnityEngine;
//using Unity.MLAgents;
//using Unity.MLAgents.Sensors;
//using Unity.MLAgents.Actuators;

//public class MachineLearning : Agent
//{
//    Rigidbody rBody;

//    void Start()
//    {
//        rBody = GetComponent<Rigidbody>();
//    }

//    public Transform Target;

//    private bool resetTargetPos = false;

//    public override void OnEpisodeBegin()
//    {
//        // If the Agent fell, zero its momentum
//        this.rBody.angularVelocity = Vector3.zero;
//        this.rBody.velocity = Vector3.zero;
//        this.transform.localPosition = new Vector3(-7, 1.5f, 0);

//        //target new pos
//        //Target.localPosition = new Vector3(Random.value * 8 - 4, 0.5f, Random.value * 8 - 4);
//        if(resetTargetPos)
//        {
//            Target.localPosition = RandomRayCast();
//            resetTargetPos = false;
//        }
        
//    }

//    public override void CollectObservations(VectorSensor sensor)
//    {
//        // Target and Agent positions
//        sensor.AddObservation(Target.localPosition);
//        sensor.AddObservation(this.transform.localPosition);

//        // Agent velocity
//        sensor.AddObservation(rBody.velocity.x);
//        sensor.AddObservation(rBody.velocity.z);
//    }

//    public float forceMultiplier = 10;
//    public override void OnActionReceived(ActionBuffers actionBuffers)
//    {
//        // Actions, size = 2
//        Vector3 controlSignal = Vector3.zero;
//        controlSignal.x = actionBuffers.ContinuousActions[0];
//        controlSignal.z = actionBuffers.ContinuousActions[1];
//        rBody.AddForce(controlSignal * forceMultiplier);

//        // Rewards
//        //float distanceToTarget = Vector3.Distance(this.transform.localPosition, Target.localPosition);

//        // Reached target
//        //if (distanceToTarget < 1.42f)
//        //{
//        //    SetReward(1.0f);
//        //    EndEpisode();
//        //}


//    }

//    public override void Heuristic(in ActionBuffers actionsOut)
//    {
//        var continuousActionsOut = actionsOut.ContinuousActions;
//        continuousActionsOut[0] = Input.GetAxis("Horizontal");
//        continuousActionsOut[1] = Input.GetAxis("Vertical");
//    }

//    void OnCollisionEnter(Collision col)
//    {
//        // Reached target
//        if (col.transform.CompareTag("Wall"))
//        {
//            //SetReward(-1.0f);
//            EndEpisode();
//        }
//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        if(other.gameObject == Target.gameObject)
//        {

//            SetReward(1.0f);
//            resetTargetPos = true;
//            EndEpisode();
//        }
//    }


//    [SerializeField] private LayerMask layerMask = 0;
//    [SerializeField] private float wanderRange = 30.0f;
//    private Vector3 RandomRayCast()
//    {
//        RaycastHit hit;
//        Vector3 origin = transform.position + Random.insideUnitSphere * wanderRange;

//        origin.y = 100.0f;
//        if (Physics.Raycast(origin, transform.TransformDirection(Vector3.down), out hit, 200.0f, layerMask))
//        {
//            Debug.DrawRay(origin, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
//        }


//        if (hit.point == new Vector3(0.0f, 0.0f, 0.0f))
//        {
//            return RandomRayCast();
//        }
//        else return (hit.point);
//    }
//}
