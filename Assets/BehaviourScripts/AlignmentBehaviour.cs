using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Alignment")]
public class AlignmentBehaviour : FlockBehaviour
{
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //If no neighbors, main current alignment
        if (context.Count == 0)
        {
            return agent.transform.up;
        }
        //Get average pos of neighbors
        Vector2 alignmentMove = Vector2.zero;
        foreach (Transform item in context)
        {
            if (item.GetComponent<FlockAgent>())
            {
                if (!agent.predator)
                {
                    if (item.GetComponent<FlockAgent>().flockId == agent.flockId)
                    {
                        alignmentMove += (Vector2)item.up;
                    }
                    else
                    {
                        alignmentMove -= (Vector2)item.up;
                    }
                }
                else
                {
                    if (item.GetComponent<FlockAgent>().flockId != agent.flockId)
                    {
                        alignmentMove += (Vector2)item.up;
                    }
                    else
                    {
                        alignmentMove -= (Vector2)item.up;
                    }
                }
            }
        }
        alignmentMove /= context.Count;

        return alignmentMove;
    }
}
