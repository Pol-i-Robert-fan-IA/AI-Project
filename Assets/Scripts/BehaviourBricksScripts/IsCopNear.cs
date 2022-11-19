using UnityEngine;

using Pada1.BBCore;
using Pada1.BBCore.Framework;

[Condition("MyConditions/Is Cop Near?")]
[Help("Checks whether Cop is near the Treasure.")]
public class IsCopNear : ConditionBase
{
    public override bool Check()
    {
        GameObject orc = GameObject.FindGameObjectWithTag("Orc");
        GameObject woundednpc = GameObject.FindGameObjectWithTag("Wounded");

        return Vector3.Distance(orc.transform.position, woundednpc.transform.position) < 10f;
    }
}