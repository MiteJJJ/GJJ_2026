using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ScriptableObject is a serializable Unity class that allows you to store large quantities of shared data independent from script instances.
public abstract class FlockBehavior : ScriptableObject
{
    public abstract Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock);
}
