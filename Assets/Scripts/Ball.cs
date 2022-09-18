using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    //TODO: Convert data to scriptable object
    [SerializeField] private float _speedLimit;
    [SerializeField] private float _startTime;
    [SerializeField] private float _minReflectionAngleNoise;
    [SerializeField] private float _maxReflectionAngleNoise;

    [SerializeField] GameObject _explosionParticleSystem;

    private AudioSource _audioSource;
    private Rigidbody _rigidbody;
    private Vector3 _lastVelocity;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
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
        _lastVelocity = _rigidbody.velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 reflectedVector = Vector3.Reflect(_lastVelocity, collision.contacts[0].normal);
        _rigidbody.velocity = (reflectedVector.normalized + AddNoiseOnAngle(_minReflectionAngleNoise, _maxReflectionAngleNoise)) * _speedLimit;

        _audioSource.Play();
    }

    public void Explode()
    {
        // Instantiate(_explosionParticleSystem);
        Destroy(gameObject);
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
        return noise;
    }
}
