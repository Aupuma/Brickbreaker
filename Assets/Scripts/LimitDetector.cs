using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitDetector : MonoBehaviour
{
    private BoxCollider _boxCollider;
    private MeshRenderer _renderer;

    public event Action<Ball> BallLost;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _renderer = GetComponent<MeshRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SetLimitActive(true);
    }


    public void SetLimitActive(bool active)
    {
        if (active)
        {
            _boxCollider.isTrigger = true;
            _renderer.enabled = false;
        }
        else
        {
            _boxCollider.isTrigger = false;
            _renderer.enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ball")
        {
            Ball ball = other.GetComponent<Ball>();
            BallLost?.Invoke(ball);
        }
    }
}
