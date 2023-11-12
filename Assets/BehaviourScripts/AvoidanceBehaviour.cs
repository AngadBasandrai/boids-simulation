using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Avoidance")]
public class AvoidanceBehaviour : FlockBehaviour
{
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //If no neighbors, no adjustment
        if (context.Count == 0)
        {
            return Vector2.zero;
        }
        //Get average pos of neighbors
        Vector2 avoidanceMove = Vector2.zero;
        int nAvoid = 0;
        foreach (Transform item in context)
        {
            if (item.GetComponent<FlockAgent>())
            {
                if (!agent.predator)
                {
                    if (item.GetComponent<FlockAgent>().predator)
                    {
                        if (Vector2.SqrMagnitude(item.position - agent.transform.position) < flock.SquareAvoidanceRadius * 2)
                        {
                            nAvoid++;
                            avoidanceMove += (Vector2)(agent.transform.position - item.position) * 14;
                        }
                    }
                    else if (item.GetComponent<FlockAgent>().flockId == agent.flockId)
                    {
                        if (Vector2.SqrMagnitude(item.position - agent.transform.position) < flock.SquareAvoidanceRadius)
                        {
                            nAvoid++;
                            avoidanceMove += (Vector2)(agent.transform.position - item.position);
                        }
                    }
                    else
                    {
                        if (Vector2.SqrMagnitude(item.position - agent.transform.position) < flock.SquareAvoidanceRadius)
                        {
                            nAvoid++;
                            avoidanceMove += (Vector2)(agent.transform.position - item.position) * 7;
                        }
                    }
                }
                else
                {                    
                    if (Vector2.SqrMagnitude(item.position - agent.transform.position) < flock.SquareAvoidanceRadius)
                    {
                        nAvoid++;
                        avoidanceMove += (Vector2)(agent.transform.position - item.position);
                    }
                }
            }

            if (item.GetComponent<Obstacle>())
            {
                nAvoid++;
                avoidanceMove += (Vector2)(agent.transform.position - item.position) * item.GetComponent<Obstacle>().avoidForce;
            }
        }

        if (nAvoid > 0)
        {
            avoidanceMove /= nAvoid;
        }

        return avoidanceMove;
    }
}
