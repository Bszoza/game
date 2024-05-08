using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOff : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer sp;
    public float targetTime = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        targetTime -= Time.deltaTime;

        if (targetTime <= 0.0f)
        {
            timerEnded();
        }
        if (targetTime <= -3.0f) {
            timerStart();
        }

    }
    void timerEnded()
    {
        rb.simulated=false;
        sp.enabled = false;
    }
    void timerStart() {
        rb.simulated = true;
        sp.enabled = true;
        targetTime = 3.0f;
    }

}
