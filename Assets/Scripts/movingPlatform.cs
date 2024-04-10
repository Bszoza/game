using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Nie moze uderzac o o inne bloki inaczej zrzuca gracza
public class movingPlatform : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    Vector3 target;
    public float speed = 4.0f;

    MovementPlayer movementPlayer;
    Rigidbody2D rb;
    Vector3 moveDirect;

    private void Awake()
    {
        movementPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementPlayer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        target = endPoint.position;
        DirectionCalculate();
    }
   

    private void Update()
    {
        if (Vector2.Distance(transform.position, startPoint.position) < 0.1f) { target = endPoint.position; DirectionCalculate(); }
        if (Vector2.Distance(transform.position, endPoint.position) < 0.1f) { target = startPoint.position; DirectionCalculate(); }
      
    }
    private void FixedUpdate()
    {
        rb.velocity = moveDirect * speed;
    }

    void DirectionCalculate() {
        moveDirect = (target - transform.position).normalized;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            movementPlayer.isTouchingMovingPlatform = true;
            movementPlayer.platformRB = rb;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        movementPlayer.isTouchingMovingPlatform = false;
    }


}
