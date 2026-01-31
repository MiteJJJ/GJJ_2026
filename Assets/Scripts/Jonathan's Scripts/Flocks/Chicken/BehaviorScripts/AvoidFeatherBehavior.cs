using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/AvoidFeather")]
public class AvoidFeatherBehavior : FlockBehavior
{
    public float avoidFeatherDistance = 5f;

    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        Vector3 avoidMove = Vector3.zero;
        int nAvoid = 0;
        GameObject[] feathers = GameObject.FindGameObjectsWithTag("Feather");
        foreach (GameObject f in feathers)
        {
            Vector3 diff = agent.transform.position - f.transform.position;
            diff.y = 0f;
            if (diff.sqrMagnitude < (avoidFeatherDistance * avoidFeatherDistance))
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
