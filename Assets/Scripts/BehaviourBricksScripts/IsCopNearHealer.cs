using UnityEngine;

using Pada1.BBCore;
using Pada1.BBCore.Framework;

[Condition("MyConditions/Is Orc Far from Healer?")]
[Help("Checks whether Orc is near the Healer.")]
public class IsCopNearHealer : ConditionBase
{
    public override bool Check()
    {
        GameObject orc = GameObject.FindGameObjectWithTag("Orc");
        GameObject healer = GameObject.FindGameObjectWithTag("Healer");

        return Vector3.Distance(orc.transform.position, healer.transform.position) > 20f;
    }
}
