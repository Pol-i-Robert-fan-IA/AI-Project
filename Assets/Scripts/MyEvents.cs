using Skeleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyEvents : MonoBehaviour
{ 
    public List<Blackboard_Skeleton> aliveSkeletons;
    public List<Blackboard_Skeleton> deathSkeletons;

    public List<Killer> killers;
    public List<Hider> hiders;

    private void Start()
    {
        Hider[] hiding = FindObjectsOfType<Hider>();

        foreach(Hider hide in hiding)
        {
            hiders.Add(hide);
        }
    }
}
