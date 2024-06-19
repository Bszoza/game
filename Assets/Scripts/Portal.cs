using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private Transform destination;
    public float distance = 0.2f;
    public Rigidbody2D other_portal;
    void Start()
    {
            destination = other_portal.GetComponent<Transform>();  
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Vector2.Distance(transform.position, other.transform.position) > distance) {
            other.transform.position = new Vector2(destination.position.x, destination.position.y);
        }
    }
}
