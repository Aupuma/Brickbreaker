using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    [SerializeField] private float _fallingSpeed;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody.velocity = Vector3.down * _fallingSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Paddle")
        {
            Collect();
        }
        else if (other.tag == "Limit")
        {
            Destroy(gameObject);
        }
    }

    public abstract void Collect();
}
