using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
     public Transform start;
    public Transform end;

    public float speed;

    private float startTime;

    private float journeyLength;

    void Start()
    {
        startTime = Time.time;

        journeyLength = Vector3.Distance(start.position, end.position);
    }

    void Update()
    {
        float distCovered = (Time.time - startTime) * speed;

        float fracJourney = distCovered / journeyLength;

        transform.position = Vector2.Lerp(start.position, end.position, Mathf.PingPong(fracJourney, 1));//change to vect2 to work with 2d movement.
    }
}
