using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpPickUp : MonoBehaviour
{
    [SerializeField] private float _fallingSpeed;

    private Rigidbody _rigidbody;

    public event Action Collected;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _rigidbody.velocity = Vector3.down * _fallingSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Paddle")
        {
            Collected?.Invoke();
            gameObject.SetActive(false);
        }

    }

}
