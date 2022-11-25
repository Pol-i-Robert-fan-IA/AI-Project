using UnityEngine;

using Pada1.BBCore;
using Pada1.BBCore.Framework;

[Condition("MyConditions/Is Cop Near?")]
[Help("Checks whether Cop is near the Treasure.")]
public class IsCopNear : ConditionBase
{

    [InParam("GameObject")]
    public GameObject self = null;

    public override bool Check()
    {
        GameObject[] orc = GameObject.FindGameObjectsWithTag("Orc");

        foreach (GameObject o in orc)
        {
            
            if (Vector3.Distance(self.transform.position, o.transform.position) < 10f)
            {
                return true;
            }
        }
        return false;
    }
}