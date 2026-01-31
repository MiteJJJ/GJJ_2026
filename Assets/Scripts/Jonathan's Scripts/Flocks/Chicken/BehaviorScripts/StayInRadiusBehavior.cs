using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Stay In Radius")]
public class StayInRadiusBehavior : FlockBehavior
{
    public float radius = 50f;
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        Vector3 centerOffset = flock.fishSpawnPoint - agent.transform.position;
        centerOffset.y = 0f;
        float t = centerOffset.magnitude / radius;
        if (t < 1f)
        {
            return Vector3.zero;
        }

        return centerOffset * t * t;
    }
}
