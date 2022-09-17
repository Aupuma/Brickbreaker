using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float _speedLimit;
    [SerializeField] private float _startTime;

    [SerializeField] GameObject _explosionParticleSystem;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Throw", _startTime);
    }

    private void Throw()
    {
       // _rigidbody.velocity = new Vector3()
    }

    private void FixedUpdate()
    {
        if (_rigidbody.velocity.magnitude > _speedLimit)
        {
            _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, _speedLimit);
        }
    }

    public void Explode()
    {
        Instantiate(_explosionParticleSystem);
        Destroy(gameObject);
    }
}
