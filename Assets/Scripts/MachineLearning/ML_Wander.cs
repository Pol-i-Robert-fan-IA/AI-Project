using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class ML_Wander : Agent
{
    private Vector3 initialPos;
    Rigidbody rBody;

    public Transform Target;
    private bool resetTargetPos = false;

    [SerializeField] float maxStopTime = 10.0f;
    [SerializeField] float currentDelay = 0.0f;
    public override void Initialize()
    {
        rBody = GetComponent<Rigidbody>();
        initialPos = transform.localPosition;
    }

    public override void OnEpisodeBegin()
    {
        // this.rBody.angularVelocity = Vector3.zero;
        //this.rBody.velocity = Vector3.zero;
        //this.transform.localPosition = initialPos;

        //Unstuck
        currentDelay = 0.0f;

        if (resetTargetPos)
        {
            Vector3 newPos = RandomRayCast();
            newPos.y += 1.5f;
            Target.position = newPos;
            resetTargetPos = false;
        }
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(Target.localPosition);
        sensor.AddObservation(transform.InverseTransformDirection(rBody.velocity));

        sensor.AddObservation(rBody.velocity.x);
        sensor.AddObservation(rBody.velocity.z);
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        AddReward(-1f / MaxStep);
        MoveAgent(actionBuffers.DiscreteActions);
        //UnStuck();
    }

    public void UnStuck()
    {
        if (rBody.velocity.z < 0.05f) currentDelay += Time.deltaTime;
        else currentDelay = 0.0f;

        if (currentDelay > maxStopTime)
        {
            resetTargetPos = true;
            currentDelay = 0.0f;

            Debug.Log("Unstucking");
            EndEpisode();
        }
    }

    public void MoveAgent(ActionSegment<int> act)
    {
        Vector3 direction = Vector3.zero;
        Vector3 rotateDir = Vector3.zero;

        var action = act[0];
        switch (action)
        {
            case 1: 
                direction = transform.forward * 1f;
                break;
            //case 2:
            //    direction = transform.forward * -1f;
            //    break;
            case 2: 
                rotateDir = transform.up * 1f;
                break;
            case 3: 
                rotateDir = transform.up * -1f;
                break;
        }

        transform.Rotate(rotateDir, Time.deltaTime * 200f);
        rBody.AddForce(direction * 2f, ForceMode.VelocityChange);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActionsOut = actionsOut.DiscreteActions;
        if (Input.GetKey(KeyCode.D))
        {
            discreteActionsOut[0] = 3;
        } else if (Input.GetKey(KeyCode.W))
        {
            discreteActionsOut[0] = 1;
        } else if (Input.GetKey(KeyCode.A))
        {
            discreteActionsOut[0] = 4;
        } else if (Input.GetKey(KeyCode.S))
        {
            discreteActionsOut[0] = 2;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        // Reached target
        if (col.transform.CompareTag("Wall"))
        {
           SetReward(-0.01f);
           EndEpisode();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("WLTARGET"))
        {
            SetReward(1.0f);
            resetTargetPos = true;
            EndEpisode();
        }
    }


    [SerializeField] private LayerMask layerMask = 0;
    [SerializeField] private float wanderRange = 30.0f;
    private Vector3 RandomRayCast()
    {
        RaycastHit hit;
        Vector3 origin = transform.position + Random.insideUnitSphere * wanderRange;

        origin.y = 100.0f;
        if (Physics.Raycast(origin, transform.TransformDirection(Vector3.down), out hit, 200.0f, layerMask))
        {
            Debug.DrawRay(origin, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
        }


        if (hit.point == new Vector3(0.0f, 0.0f, 0.0f))
        {
            return RandomRayCast();
        }
        else return (hit.point);
    }
}
