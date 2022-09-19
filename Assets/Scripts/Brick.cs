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
    private int _hitsTaken = 0;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            TakeHit();
            AddScore();
        }
    }

    private void AddScore()
    {
        BrickScoreUI scoreInstance = Instantiate(_scorePrefab, _scoreSpawnTransform.position,Quaternion.identity);
        scoreInstance.SetScore(_data.Score);

        GameManager.instance.AddScore(_data.Score);
    }

    private void TakeHit()
    {
        _hitsTaken++;
        if(_hitsTaken == _data.HitsToDestroy)
        {
            
            //Instantiate(_data.DestructionParticleSystem);

            float powerupChance = UnityEngine.Random.Range(0f, 1f);
            if(powerupChance <= _data.PowerupSpawnChance && _data.PowerUpToSpawn!=null)
            {
                Instantiate(_data.PowerUpToSpawn, transform.position,Quaternion.identity);
            }
            
            BoardManager.instance.DecreaseBricks();
            Destroy(gameObject);
        }
    }
}
