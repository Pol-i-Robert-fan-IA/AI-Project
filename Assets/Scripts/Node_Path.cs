using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Node_Path : MonoBehaviour
{

    Node[] nodes;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Node[] GetPathNodes()
    {
        return GetComponentsInChildren<Node>();
    }

    private void OnValidate()
    {
        nodes = GetPathNodes();
    }

    [ExecuteInEditMode]
    private void OnDrawGizmos()
    {
        for(int i = 0; i < nodes.Length; i++)
        {
            if (i == nodes.Length-1)
            {
                Debug.DrawLine(nodes[i].transform.position, nodes[0].transform.position);
            }
            else
            {
                Debug.DrawLine(nodes[i].transform.position, nodes[i + 1].transform.position);
            }
        }
    }
}
