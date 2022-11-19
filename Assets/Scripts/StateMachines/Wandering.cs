using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skeleton
{
    public class Wandering : StateMachineBehaviour
    {
        Move move;
        Blackboard_Skeleton blackboard;

        private float currentDelay = Mathf.Infinity;
        private float delay = 0.5f;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            move = animator.GetComponent<Move>();
            blackboard = animator.GetComponent<Blackboard_Skeleton>();
            blackboard.point = move.NewPoint();
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {;
            //When close to the point
            if (Vector3.Distance(blackboard.point, animator.transform.position) < 2f)
            {
                Debug.Log("Wander-New point");
                blackboard.point = move.NewPoint();
            }

            if (currentDelay > delay)
            {
                if(CorpseCheck())
                {
                    animator.SetTrigger("Near");
                }
            }
            currentDelay += Time.deltaTime;
        }

        private bool CorpseCheck()
        {
            bool toReturn = false;
            Collider[] rangeChecks = Physics.OverlapSphere(move.transform.position, blackboard.detectionRadius, blackboard.corpseMask);

            if (rangeChecks.Length != 0)
            {
                Transform target = rangeChecks[0].transform;
                Vector3 directionToTarget = (target.position - move.transform.position).normalized;

                if (Vector3.Angle(move.transform.forward, directionToTarget) < blackboard.viewAngle / 2)
                {
                    Debug.Log(target.position);
                    blackboard.corpse = target;
                    toReturn =  true;
                }
            }
            return toReturn;
        }
    }
}

