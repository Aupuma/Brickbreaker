using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitDetector : MonoBehaviour
{
    private BoxCollider _boxCollider;

    public event Action<Ball> BallLost;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ball")
        {
            Ball ball = other.GetComponent<Ball>();
            ball.Explode();
            BallLost?.Invoke(ball);
        }
    }
}
