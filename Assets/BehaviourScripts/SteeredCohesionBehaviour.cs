using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Steered Cohesion")]
public class SteeredCohesionBehaviour : FlockBehaviour
{

    Vector2 currentVelocity;
    public float agentSmoothTime = 0.5f;

    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //If no neighbors, no adjustment
        if (context.Count == 0)
        {
            return Vector2.zero;
        }
        //Get average pos of neighbors
        Vector2 cohesionMove = Vector2.zero;
        foreach (Transform item in context)
        {
            if (item.GetComponent<FlockAgent>())
            {
                if (!agent.predator)
                {
                    if (item.GetComponent<FlockAgent>().flockId == agent.flockId)
                    {
                        cohesionMove += (Vector2)item.position;
                    }
                }
                else
                {
                    if (item.GetComponent<FlockAgent>().flockId != agent.flockId)
                    {
                        cohesionMove += (Vector2)item.position;
                    }

                }
            }
        }
        cohesionMove /= context.Count;

        //offset from agent pos
        cohesionMove -= (Vector2)agent.transform.position;
        cohesionMove = Vector2.SmoothDamp(agent.transform.up, cohesionMove, ref currentVelocity, agentSmoothTime);
        return cohesionMove;
    }
}
