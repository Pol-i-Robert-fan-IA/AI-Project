using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flocking_Manager_AI : MonoBehaviour
{
    [HideInInspector] public Flocking_Leader_AI leader;

    [SerializeField] GameObject prefab;
    [SerializeField] int size = 20;

    [Header("")]
    public float neighbourDistance = 30.0f;
    [Range(1.0f, 20.0f)] public float minSpeed = 2.0f;
    [Range(1.0f, 20.0f)] public float maxSpeed = 8.0f;

    [Header("")]
    [Range(0.05f, 1.0f)] public float rotationSpeed = 3.0f;

    [Header("")]
    public float delayMin = 2.0f;
    public float delayMax = 3.5f;

    [SerializeField] Vector3 initMinPos;
    [SerializeField] Vector3 initMaxPos;
    [Header("")]
    public bool debug = false;

    [HideInInspector] public GameObject[] school;

    private void Awake()
    {
        leader = GetComponentInChildren<Flocking_Leader_AI>();

        leader.GetComponent<Flocking_AI>().myManager = this;
    }

    void Start()
    {
        school = new GameObject[size];
        for (int i = 0; i < size; ++i)
        {
            Vector3 pos = transform.position;
            pos += new Vector3(Random.Range(initMinPos.x, initMaxPos.x), Random.Range(initMinPos.y, initMaxPos.y), Random.Range(initMinPos.z, initMaxPos.z));  // random position

            Vector3 randomize = Random.insideUnitSphere; // random vector direction

            school[i] = (GameObject)Instantiate(prefab, pos,  Quaternion.LookRotation(randomize));
            school[i].GetComponent<Flocking_AI>().myManager = this;

            school[i].transform.parent = gameObject.transform;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0.0f, 0.0f, 1.0f, 0.8f);
        Gizmos.DrawCube(transform.position, new Vector3(1.0f, 1.0f, 1.0f));
    }
}
