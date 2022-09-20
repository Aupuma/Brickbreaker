using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] private BrickScoreUI _scorePrefab;
    [SerializeField] private Transform _scoreSpawnTransform;

    [SerializeField] private BrickData _data;

    private Collider _collider;
    private int _hitsTaken = 0;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            AddScore(_data.Score);
            TakeHit();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ball")
        {
            AddScore(_data.Score * _data.HitsToDestroy);
            Explode();
        }
    }

    public void SetColliderToTrigger(bool isTrigger)
    {
        _collider.isTrigger = isTrigger;
    }

    private void AddScore(int score)
    {
        BrickScoreUI scoreInstance = Instantiate(_scorePrefab, _scoreSpawnTransform.position,Quaternion.identity);
        scoreInstance.SetScore(score);

        GameManager.instance.AddScore(score);
    }

    private void TakeHit()
    {
        _hitsTaken++;
        if(_hitsTaken == _data.HitsToDestroy)
        {
            Explode();
        }
    }

    private void Explode()
    {
        //Instantiate(_data.DestructionParticleSystem);
        SpawnPickupOnChance();
        BrickManager.instance.RemoveBrick(this);
        Destroy(gameObject);
    }

    private void SpawnPickupOnChance()
    {
        float pickupChance = UnityEngine.Random.Range(0f, 1f);
        if (pickupChance <= _data.PowerupSpawnChance && _data.PickupToSpawn != null)
        {
            Instantiate(_data.PickupToSpawn, transform.position, Quaternion.identity);
        }
    }
}
