using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public float jumpPower = 9.0f;
    [SerializeField] Rigidbody2D rb;
    
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower * 1.80f);
        }
    }
}
