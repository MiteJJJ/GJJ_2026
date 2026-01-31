using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Alignment")]
public class AlignmentBehavior : FlockBehavior
{
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        // if no neighbors, maintain current forward
        if (context.Count == 0)
            return agent.transform.forward;

        // add all forward vectors together and average (XZ plane)
        Vector3 alignmentMove = Vector3.zero;
        foreach (Transform item in context)
        {
            Vector3 f = item.transform.forward;
            f.y = 0f;
            alignmentMove += f;
        }
        alignmentMove /= context.Count;
        alignmentMove.y = 0f;

        return alignmentMove;
    }
}
