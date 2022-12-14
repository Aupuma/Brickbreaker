using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUpEffect : MonoBehaviour
{
    [SerializeField] private float _duration;

    private bool _isEffectActive;
    private float _effectTimer;

    private void Start()
    {
        ActivateEffect();
    }

    // Update is called once per frame
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

    public void ActivateEffect()
    {
        _isEffectActive = true;
        _effectTimer = _duration;
        StartEffect();
    }

    public void DeactivateEffect()
    {
        _isEffectActive = false;
        EndEffect();
        Destroy(gameObject);
    }

    protected abstract void StartEffect();
    protected abstract void EndEffect();
}
