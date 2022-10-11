using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{


    [ExecuteInEditMode]
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1.0f,0.0f,1.0f,0.5f);
        Gizmos.DrawSphere(transform.position, 0.5f);
    }
}
