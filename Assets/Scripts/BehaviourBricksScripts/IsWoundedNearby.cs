using UnityEngine;

using Pada1.BBCore;
using Pada1.BBCore.Framework;

[Condition("MyConditions/Is Wounded Nearby?")]
[Help("Checks whether a Wounded is near the Healer.")]
public class IsWoundedNearby : ConditionBase
{

    [OutParam("Wounded Seen?")]
    public bool seen;
    public override bool Check()
    {
        GameObject Healer = GameObject.FindGameObjectWithTag("Healer");
        GameObject woundednpc = GameObject.FindGameObjectWithTag("Wounded");

        seen = (Vector3.Distance(Healer.transform.position, woundednpc.transform.position) < 25f);

        return Vector3.Distance(Healer.transform.position, woundednpc.transform.position) < 25f;
    }
}
