using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost_AI : MonoBehaviour
{
    [Header("Path")]
    [SerializeField] Node_Path path;
    Node[] node;
    [Header("Follower")]
    [SerializeField] GameObject wanderEvade;
    [Header("Variables")]
    [SerializeField] float stopArea;
    [SerializeField] float pursuingArea;
    [SerializeField] float speed = 0.0f;
    int currentNode = 0;
    Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        node = path.GetPathNodes();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 temp = Vector3.zero;
        if (Vector3.Distance(wanderEvade.transform.position, transform.position) < pursuingArea)
        {
            if (Vector3.Distance(node[currentNode].transform.position, transform.position) < stopArea)
            {
                if (currentNode == (path.GetPathNodes().Length - 1))
                {
                    currentNode = 0;
                }
                else
                {
                    currentNode++;
                }
            }
            temp = (node[currentNode].transform.position - this.transform.position).normalized * speed;
        }

        direction = (temp) * Time.deltaTime;
        direction.y = 0.0f;
        this.transform.position += direction;
    }
}
