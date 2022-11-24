using Skeleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Approaching : StateMachineBehaviour
{
    Move move;
    Blackboard_Skeleton blackboard;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetTrigger("Near");
        move = animator.GetComponent<Move>();
        blackboard = animator.GetComponent<Blackboard_Skeleton>();
        animator.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = 2f;
        move.Displace(blackboard.corpse.position);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (blackboard.isDead) animator.SetBool("isDead", true);

        if (Vector3.Distance(move.transform.position, blackboard.corpse.position) < blackboard.dist2Eat)
        {
            move.StopAgent();
            animator.SetTrigger("Eat");
        };
    }
}
