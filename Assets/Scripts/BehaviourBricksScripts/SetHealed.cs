using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Actions
{
    [Action("MyActions/SetHealed")]
    [Help("Set bool Healed to true")]
    public class SetHealed : GOAction
    {
        
        [InParam("Healed")]
        [Help("true if must be activate")]
        public bool healed;

        ///<value>Input Game object to set the active value Parameter.</value>
        [InParam("game object")]
        [Help("Game object to set the active value, if no assigned the active value will be set to the game object of this behavior")]
        public GameObject targetGameobject;

        /// <summary>Initialization Method of SetActive.</summary>
        /// <remarks>Activate or deactivate the GameObject.</remarks>
        public override void OnStart()
        {
            
        }

        /// <summary>Method of Update of SetActive.</summary>
        /// <remarks>Complete the task.</remarks>
        public override TaskStatus OnUpdate()
        {
            return TaskStatus.COMPLETED;
        }
    }
}