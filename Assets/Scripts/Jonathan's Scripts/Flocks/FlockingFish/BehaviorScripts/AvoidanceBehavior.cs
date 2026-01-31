using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Avoidance")]
public class AvoidanceBehavior : FlockBehavior
{
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //if no neighbors, return no adjustment
        if (context.Count == 0)
            return Vector3.zero;
        // add all points together and average on XZ plane
        Vector3 avoidanceMove = Vector3.zero;
        int nAvoid = 0;
        foreach (Transform item in context)
        {
            Vector3 diff = agent.transform.position - item.position;
            diff.y = 0f;
            if (diff.sqrMagnitude < flock.SquareAvoidanceRadius)
            {
                nAvoid++;
                avoidanceMove += diff;
            }
        }
        if (nAvoid > 0)
            avoidanceMove /= nAvoid;

        avoidanceMove.y = 0f;
        return avoidanceMove;
    }
}
