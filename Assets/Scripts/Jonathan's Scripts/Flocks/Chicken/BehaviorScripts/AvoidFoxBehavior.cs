using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/AvoidFox")]
public class AvoidFoxBehavior : FlockBehavior
{
    public float avoidFoxDistance = 5f;

    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        Vector3 avoidMove = Vector3.zero;
        int nAvoid = 0;
        GameObject[] foxes = GameObject.FindGameObjectsWithTag("Fox");
        foreach (GameObject f in foxes)
        {
            Vector3 diff = agent.transform.position - f.transform.position;
            diff.y = 0f;
            if (diff.sqrMagnitude < (avoidFoxDistance * avoidFoxDistance))
            {
                nAvoid++;
                avoidMove += diff;
            }
        }
        if (nAvoid > 0)
            avoidMove /= nAvoid;

        avoidMove.y = 0f;
        return avoidMove;
    }
}
