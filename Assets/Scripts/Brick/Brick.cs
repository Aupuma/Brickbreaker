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
        PickupManager.instance.SpawnPickupOnChance(_data.PickupToSpawn, _data.PowerupSpawnChance, transform.position);
        BrickManager.instance.RemoveBrick(this);
        Destroy(gameObject);
    }
}
