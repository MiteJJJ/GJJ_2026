using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/AvoidEgg")]
public class AvoidEggBehavior : FlockBehavior
{
    public float avoidEggDistance = 5f;

    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        Vector3 avoidMove = Vector3.zero;
        int nAvoid = 0;
        GameObject[] eggs = GameObject.FindGameObjectsWithTag("Egg");
        foreach (GameObject e in eggs)
        {
            Vector3 diff = agent.transform.position - e.transform.position;
            diff.y = 0f;
            if (diff.sqrMagnitude < (avoidEggDistance * avoidEggDistance))
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
