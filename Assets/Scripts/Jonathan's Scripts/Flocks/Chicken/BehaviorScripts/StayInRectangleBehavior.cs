using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Stay In Rectangle")]
public class StayInRectangleBehavior : FlockBehavior
{
    public float height = 10f;
    public float width = 30f;

    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        Vector3 centerOffset = flock.fishSpawnPoint - agent.transform.position;
        centerOffset.y = 0f;
        float heightOffset = centerOffset.z;
        float widthOffset = centerOffset.x;

        if (Mathf.Abs(heightOffset) / height < 1f && Mathf.Abs(widthOffset) / width < 1f)
        {
            return Vector3.zero;
        }

        return centerOffset;
    }
}