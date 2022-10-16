using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Flocking_AI : MonoBehaviour
{

    [HideInInspector] public Flocking_Manager_AI myManager;

    float speed = 6.0f;

    public Vector3 direction;

    private float currentDelay = Mathf.Infinity;
    private float delay = 0.5f;


    private float followSpeed = 20.0f;
    private float rotationSpeed = 5.0f;

    private float finalSpeed = 0.0f;
    private float finalRotationSpeed = 0.0f;

    [HideInInspector] public Transform followingTarget;

    // Start is called before the first frame update
    void Start()
    {
        direction = new Vector3(0, 0, 0);

        delay = Random.Range(myManager.delayMin, myManager.delayMax);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentDelay > delay)
        {
            direction = (Cohesion() + VelocityAndAlign() + Separation() + Following()).normalized * speed;
            currentDelay = 0.0f;
        }
        currentDelay += Time.deltaTime;
        
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), finalRotationSpeed * Time.deltaTime);
        transform.Translate(0.0f, 0.0f, Time.deltaTime * finalSpeed);
    }

    private Vector3 Cohesion()
    {
        Vector3 cohesion = Vector3.zero;
        int num = 0;

        foreach (GameObject go in myManager.school)
        {
            if (go != this.gameObject)
            {
                float distance = Vector3.Distance(go.transform.position, transform.position);
                if (distance <= myManager.neighbourDistance)
                {
                    cohesion += go.transform.position;
                    num++;
                }
            }
        }

        if (num > 0)
            cohesion = (cohesion / num - transform.position).normalized * speed;

        return cohesion;
    }

    private Vector3 VelocityAndAlign()
    {
        Vector3 align = Vector3.zero;
        int num = 0;
        foreach (GameObject go in myManager.school)
        {
            if (go != this.gameObject)
            {
                float distance = Vector3.Distance(go.transform.position, transform.position);
                if (distance <= myManager.neighbourDistance)
                {
                    align += go.GetComponent<Flocking_AI>().direction;
                    num++;
                }
            }
        }
        if (num > 0)
        {
            align /= num;
            speed = Mathf.Clamp(align.magnitude, myManager.minSpeed, myManager.maxSpeed);
        }

        return align;
    }

    private Vector3 Separation()
    {
        Vector3 separation = Vector3.zero;

        foreach (GameObject go in myManager.school)
        {
            if (go != this.gameObject)
            {
                float distance = Vector3.Distance(go.transform.position, transform.position);
                if (distance <= myManager.neighbourDistance)
                    separation -= (transform.position - go.transform.position) / (distance * distance);
            }
        }

        return separation;
    }

    private Vector3 Following()
    {
        if (followingTarget == null) followingTarget = myManager.leader.transform;

        float value = Vector3.Distance(transform.position, followingTarget.position);
        
        finalSpeed = Mathf.Lerp(speed, followSpeed, (value / 100));
        finalRotationSpeed = Mathf.Lerp(myManager.rotationSpeed, rotationSpeed, (value / 100));


        Vector3 vector = Vector3.zero;

        vector = myManager.leader.transform.position - transform.position;

        return vector;
    }

    private void OnDrawGizmos()
    {
        if (myManager == null) return;
        if(myManager.debug)
            Debug.DrawLine(transform.position, (transform.position + direction));
    }
}
