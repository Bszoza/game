using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOff : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer sp;
    public float timer= 3.0f;
    float targetTime;
    // Start is called before the first frame update
    void Start()
    {
        targetTime = timer;
    }

    // Update is called once per frame
    void Update()
    {
        targetTime -= Time.deltaTime;

        if (targetTime <= 0.0f)
        {
            timerEnded();
        }
        if (targetTime <= -timer) {
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
        targetTime = timer;
    }

}
