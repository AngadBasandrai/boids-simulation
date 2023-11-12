using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{

    public GameObject obstacle;

    [Range(0, 20)]
    public int spawnAmount = 5;

    [Range(0, 1000)]
    public int range;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            GameObject obs = Instantiate(
                obstacle,
                Random.insideUnitCircle * spawnAmount * (range/spawnAmount),
                Quaternion.Euler(Vector3.forward * Random.Range(0, 360)),
                transform
                );
            obs.name = "Obstacle " + i;
        }
    }
}
