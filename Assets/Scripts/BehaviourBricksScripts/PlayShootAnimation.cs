using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Actions
{
    /// <summary>
    /// It is an action to play an animation in the GameObject
    /// </summary>
    [Action("MyAnimations/PlayShootAnimation")]
    [Help("Plays an animation in the game object")]
    public class PlayShootAnimation : GOAction
    {  
        public GameObject target;

        private UnityEngine.AI.NavMeshAgent navAgent;

        Animator animator;
        /// <summary>Initialization Method of PlayAnimation.</summary>
        /// <remarks>Associate and Inacialize the animation and the elapsed time.</remarks>
        public override void OnStart()
        {
            navAgent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
            navAgent.isStopped = true;
            animator = gameObject.GetComponentInChildren<Animator>();
            animator.Play("Base Layer.Shoot");
            Debug.Log("Play anim");
            target = GameObject.FindGameObjectWithTag("Wounded");

            //animator[animationClip.name].wrapMode = animationWrap;
            //animator.CrossFade(animationClip.name, crossFadeTime);

            //elapsedTime = Time.time;
        }
        /// <summary>Method of Update of PlayAnimation.</summary>
        /// <remarks>Increase the elapsed time and check if the animation is over.</remarks>
        public override TaskStatus OnUpdate()
        {
            

            gameObject.transform.LookAt(target.transform, target.transform.position - gameObject.transform.position);
        
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Shoot") && !animator.IsInTransition(0))
            {
                Debug.Log("Animation finished");
                navAgent.isStopped = false;
                return TaskStatus.COMPLETED;
            }
                
            return TaskStatus.RUNNING;
        }
    }
}
