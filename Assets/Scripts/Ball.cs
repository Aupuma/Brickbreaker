using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float _speedLimit;
    [SerializeField] private float _startTime;
    [SerializeField] private float _minReflectionAngleNoise;
    [SerializeField] private float _maxReflectionAngleNoise;

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
        //_rigidbody.velocity = _rigidbody.velocity.normalized * _speedLimit;
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
        Vector3 reflectedVector = Vector3.Reflect(_lastVelocity, collision.contacts[0].normal);
        _rigidbody.velocity = (reflectedVector.normalized + AddNoiseOnAngle(_minReflectionAngleNoise, _maxReflectionAngleNoise)) * _speedLimit;
    }

    Vector3 AddNoiseOnAngle(float min, float max)
    {
        // Find random angle between min & max inclusive
        float xNoise = Random.Range(min, max);
        float yNoise = Random.Range(min, max);
        float zNoise = Random.Range(min, max);

        // Convert Angle to Vector3
        Vector3 noise = new Vector3(
          Mathf.Sin(2 * Mathf.PI * xNoise / 360),
          Mathf.Sin(2 * Mathf.PI * yNoise / 360),
          Mathf.Sin(2 * Mathf.PI * zNoise / 360)
        );
        Debug.Log(noise);
        return noise;
    }
}
