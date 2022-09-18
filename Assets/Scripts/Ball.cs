using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] BallData _data;
    [SerializeField] GameObject _explosionParticleSystem;
    [SerializeField] AudioClip _bounceClip;

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
        Invoke("Throw", _data.WaitTime);
    }

    private void Throw()
    {
        float randomAngle = 90 + Random.Range(-_data.MaxStartAngleOffset, _data.MaxStartAngleOffset);
        randomAngle *= Mathf.Deg2Rad;

        _rigidbody.velocity = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)).normalized * BallData.Speed;
    }

    private void FixedUpdate()
    {
        _lastVelocity = _rigidbody.velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Bounce(collision);
        _audioSource.PlayOneShot(_bounceClip);
    }

    private void Bounce(Collision collision)
    {
        Vector3 reflectedVector = Vector3.Reflect(_lastVelocity, collision.contacts[0].normal);

        if (collision.gameObject.tag == "Paddle")
        {
            Paddle paddle = collision.gameObject.GetComponent<Paddle>();
            _rigidbody.velocity = reflectedVector.normalized + paddle.GetSpeedVector();
        }
        else
        {
            _rigidbody.velocity = reflectedVector.normalized + AddNoiseOnAngle(_data.MinReflectionAngleNoise, _data.MaxReflectionAngleNoise);
        }
        _rigidbody.velocity = _rigidbody.velocity.normalized * BallData.Speed;
    }

    public void Explode()
    {
        // Instantiate(_explosionParticleSystem);
        Destroy(gameObject);
    }

    private Vector3 AddNoiseOnAngle(float min, float max)
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
