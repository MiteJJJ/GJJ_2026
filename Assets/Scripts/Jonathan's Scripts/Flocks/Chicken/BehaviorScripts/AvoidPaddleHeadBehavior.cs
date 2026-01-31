using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/AvoidPaddleHead")]
public class AvoidPaddleHeadBehavior : FlockBehavior
{
    //assets can't reference scene objects
    //public Transform paddleHead;

    public float avoidPaddleHeadDistance;
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        Vector3 avoidPaddleHeadMove = Vector3.zero;
        Vector3 diff = agent.transform.position - flock.egg.position;
        diff.y = 0f;
        if (diff.sqrMagnitude < (avoidPaddleHeadDistance * avoidPaddleHeadDistance))
        {
            avoidPaddleHeadMove = diff;
        }
        avoidPaddleHeadMove.y = 0f;
        return avoidPaddleHeadMove;
    }
}
