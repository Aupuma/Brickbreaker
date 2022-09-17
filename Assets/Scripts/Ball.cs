using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float _speedLimit;
    [SerializeField] private float _startTime;

    [SerializeField] GameObject _explosionParticleSystem;

    private Rigidbody _rigidbody;
    private Vector3 _lastVelocity;

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
        float randomAngle = Random.Range(-30f, 30f);
        randomAngle *= Mathf.Deg2Rad;
        _rigidbody.velocity = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)) * _speedLimit;
    }

    private void FixedUpdate()
    {
        //if (_rigidbody.velocity.magnitude > _speedLimit)
        //{
        //    _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, _speedLimit);
        //}
        _lastVelocity = _rigidbody.velocity;
    }

    public void Explode()
    {
       // Instantiate(_explosionParticleSystem);
        Destroy(gameObject);
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    float randomAngle = Random.Range(0,360f);
    //    randomAngle *= Mathf.Deg2Rad;
    //    _rigidbody.velocity = -collision.contacts[0].normal + new Vector3(Random.Range(-1, 1), Random.Range(-1, 1)) * _speedLimit;
    //}


    private void OnCollisionEnter(Collision collision)
    {
        float offsetAngle = Random.Range(-5, 5f);
        offsetAngle *= Mathf.Deg2Rad;
        Vector3 reflectedVector = Vector3.Reflect(_lastVelocity, collision.contacts[0].normal);
        _rigidbody.velocity = reflectedVector.normalized  * _speedLimit;
    }
}
