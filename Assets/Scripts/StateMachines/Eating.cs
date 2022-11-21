using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Skeleton;
using UnityEditor.PackageManager;

public class Eating : StateMachineBehaviour
{
    Move moves;

    MyEvents myEvents;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        myEvents = GameObject.FindObjectOfType<MyEvents>();
        moves = animator.GetComponent<Move>();
        myEvents.skeletons.Add(animator.GetComponent<Blackboard_Skeleton>());
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        myEvents = GameObject.FindObjectOfType<MyEvents>();
        myEvents.skeletons.Remove(animator.GetComponent<Blackboard_Skeleton>());
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       // moves.Action(); //Hide()
    }
}
