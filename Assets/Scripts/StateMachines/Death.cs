using Skeleton;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class Death : StateMachineBehaviour
{
    Move move;

    Blackboard_Skeleton board;

    MyEvents myEvents;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        board = animator.GetComponent<Blackboard_Skeleton>();
        myEvents = GameObject.FindObjectOfType<MyEvents>();
        move = animator.GetComponent<Move>();
        myEvents.deathSkeletons.Add(animator.GetComponent<Blackboard_Skeleton>());

        move.StopAgent();
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        move.StartAgent();
        //It is deleted from the list at the "RessSkeleton.cs" bb script.
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!board.isDead) animator.SetBool("isDead", false);
    }
}