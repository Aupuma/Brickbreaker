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

    private void FixedUpdate()
    {
        _lastVelocity = _rigidbody.velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Bounce(collision);
        _audioSource.PlayOneShot(_bounceClip);
    }

    public void Explode()
    {
        // Instantiate(_explosionParticleSystem);
        Destroy(gameObject);
    }

    private void Throw()
    {
        float randomAngle = 90 + Random.Range(-_data.MaxStartAngleOffset, _data.MaxStartAngleOffset);
        randomAngle *= Mathf.Deg2Rad;

        _rigidbody.velocity = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)).normalized * BallData.Speed;
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
            _rigidbody.velocity = reflectedVector.normalized + GetRemainingVector(collision.contacts[0].normal, reflectedVector);
        }
        _rigidbody.velocity = _rigidbody.velocity.normalized * BallData.Speed;
    }

    /// <summary>
    /// Gets the remaining vector so bounces don't get stuck on one direction
    /// </summary>
    /// <param name="surfaceNormal"></param>
    /// <param name="reflectedVector"></param>
    /// <returns></returns>
    private Vector3 GetRemainingVector(Vector3 surfaceNormal, Vector3 reflectedVector)
    {
        float angleBetweenReflectedAndNormal = Vector2.Angle(reflectedVector, surfaceNormal);
        if (angleBetweenReflectedAndNormal < _data.MinReflectionVector)
        {
            float angleToAdd = _data.MinReflectionVector - angleBetweenReflectedAndNormal;
            return new Vector3(Mathf.Sign(reflectedVector.x) * Mathf.Cos(Mathf.Deg2Rad * angleToAdd), Mathf.Sign(reflectedVector.y) * Mathf.Sin(Mathf.Deg2Rad * angleToAdd), 0f);
        }
        return Vector3.zero;
    }
}
