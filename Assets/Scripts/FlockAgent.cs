using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FlockAgent : MonoBehaviour
{

    Collider2D agentCollider;
    public Collider2D AgentCollider { get { return agentCollider; } }
    public string flockId;
    public bool predator;

    // Start is called before the first frame update
    void Start()
    {
        agentCollider = GetComponent<Collider2D>();
        Invoke("Die", 25);
        InvokeRepeating("Reproduce", Random.Range(6f, 9f), Random.Range(6f, 9f));
    }

    public void Move(Vector2 velocity)
    {
        transform.up = velocity;
        transform.position += (Vector3)velocity * Time.deltaTime;
    }

    void Reproduce()
    {
        if (Random.Range(0f, 1f) < .3f)
        {
            GameObject x = Instantiate(gameObject, transform.position + (Vector3)Random.insideUnitCircle, Quaternion.identity);
            x.transform.parent = transform.parent;
            transform.parent.GetComponent<Flock>().agents.Add(x.GetComponent<FlockAgent>());
        }
    }

    void Die()
    {
        transform.parent.GetComponent<Flock>().agents.Remove(this);
        Destroy(gameObject);
    }
}
