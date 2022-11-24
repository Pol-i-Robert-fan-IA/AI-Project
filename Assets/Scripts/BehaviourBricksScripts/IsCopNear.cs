using UnityEngine;

using Pada1.BBCore;
using Pada1.BBCore.Framework;

[Condition("MyConditions/Is Cop Near?")]
[Help("Checks whether Cop is near the Treasure.")]
public class IsCopNear : ConditionBase
{

    [InParam("Skeleton")]
    public GameObject skeleton = null;

    public override bool Check()
    {
        GameObject orc = GameObject.FindGameObjectWithTag("Orc");

        return Vector3.Distance(orc.transform.position, skeleton.transform.position) < 10f;
    }
}