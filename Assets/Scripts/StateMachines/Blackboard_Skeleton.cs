using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Skeleton
{
    public class Blackboard_Skeleton : MonoBehaviour
    {
        public float dist2Eat = 1.0f;
        [Range(1, 20)]public float detectionRadius = 1.0f;
        [Range(1, 360)] public float viewAngle = 120.0f;
        public LayerMask corpseMask = 0;
        public Transform corpse;
        public Vector3 point;
        public Animator animator;
        public bool isDead = false;

        private void Start()
        {
            animator = GetComponentInChildren<Animator>();
        }

        private void OnDrawGizmos()
        {
            Handles.color = Color.white;
            Handles.DrawWireArc(transform.position, Vector3.up, Vector3.forward, 360, detectionRadius);

            Vector3 viewAngle01 = DirectionFromAngle(transform.eulerAngles.y, -viewAngle / 2);
            Vector3 viewAngle02 = DirectionFromAngle(transform.eulerAngles.y, viewAngle / 2);

            Handles.color = Color.yellow;
            Handles.DrawLine(transform.position, transform.position + viewAngle01 * detectionRadius);
            Handles.DrawLine(transform.position, transform.position + viewAngle02 * detectionRadius);

        }

        private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
        {
            angleInDegrees += eulerY;

            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }
    }
}

