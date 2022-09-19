using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private float _fallingSpeed;

    private bool _isEffectActive;
    private float _effectTimer;
    private Rigidbody _rigidbody;
    private MeshRenderer _renderer;
    private Collider _collider;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<MeshRenderer>();
        _collider = GetComponent<Collider>();
    }

    private void Start()
    {
        _rigidbody.velocity = Vector3.down * _fallingSpeed;
    }

    void Update()
    {
        if (_isEffectActive)
        {
            _effectTimer -= Time.deltaTime;
            if (_effectTimer <= 0f)
            {
                DeactivateEffect();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Paddle")
        {
            _rigidbody.velocity = Vector3.zero;
            _renderer.enabled = false;
            _collider.enabled = false;

            ActivateEffect();
        }
        else if(other.tag == "Limit")
        {
            Destroy(gameObject);
        }
    }

    public void ActivateEffect()
    {
        _isEffectActive = true;
        _effectTimer = _duration;
        StartEffect();
    }

    private void DeactivateEffect()
    {
        _isEffectActive = false;
        EndEffect();
        Destroy(gameObject);
    }

    protected abstract void StartEffect();
    protected abstract void EndEffect();
}
