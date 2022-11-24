using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Skeleton;
using UnityEditor.PackageManager;

public class Eating : StateMachineBehaviour
{
    Blackboard_Skeleton blackboard;

    MyEvents myEvents;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        blackboard = animator.GetComponent<Blackboard_Skeleton>();

        myEvents = GameObject.FindObjectOfType<MyEvents>();
        myEvents.aliveSkeletons.Add(animator.GetComponent<Blackboard_Skeleton>());
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (blackboard.isDead) animator.SetBool("isDead", true);
    }
}
