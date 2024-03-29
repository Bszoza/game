using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatform : MonoBehaviour
{
    public Transform platform;
    public Transform startPoint;
    public Transform endPoint;
    public float speed = 1f;
    bool direction = true;
    float distance;

    private void Update()
    {
        Vector2 target = currentMovementTarget();
        platform.position = Vector2.Lerp(platform.position, target, speed * Time.deltaTime);//miedzy jednym pkt a dfrugim z predkoscia
        distance = (target - (Vector2)platform.position).magnitude;
        if (distance <= 0.1f) {
            direction=!direction;
        }
    }

    Vector2 currentMovementTarget() {
        if (direction == true) {
            return startPoint.position;
        }
        else {
            return endPoint.position;
        }

    }

    private void whenToMove() {
        if (platform != null && startPoint != null && endPoint != null) {
            Gizmos.DrawLine(platform.transform.position, startPoint.position);
            Gizmos.DrawLine(platform.transform.position, endPoint.position);
        }
    }

}
