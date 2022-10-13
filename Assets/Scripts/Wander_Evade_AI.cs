using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander_Evade_AI : MonoBehaviour
{
    [Header("Path")]
    [SerializeField] Node_Path path;
    [Header("Ghost")]
    [SerializeField] GameObject ghost;
    [Header("Runner")]
    [SerializeField] GameObject runner;
    [Header("Variables")]
    [Range(5.0f, 10.0f)]
    [SerializeField] float evadeArea;
    [Range(5.0f, 10.0f)]
    [SerializeField] float smoothArea;
    [Range(2.0f, 8.0f)]
    [SerializeField] float speed = 0.0f;
    [SerializeField] float rotationspeed = 3.0f;
    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 followghost = Vector3.zero;
        Vector3 evasion = Vector3.zero;

        if (Vector3.Distance(runner.transform.position, this.transform.position) > evadeArea)
        {
            //Vector From the Gameobject that has this script to Ghost
            followghost = (ghost.transform.position - this.transform.position);
        }
        else
        {
            //Vector From runner to Gameobject that has this script
            evasion = (this.transform.position - runner.transform.position);
        }

        direction = (followghost + evasion).normalized * speed ;
        direction.y = 0.0f;
        
        this.transform.position += direction * Time.deltaTime;

        float angle = Mathf.Rad2Deg * Mathf.Atan2(direction.x, direction.z);
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);  // up = y

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationspeed);

        

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, evadeArea);
    }
}
