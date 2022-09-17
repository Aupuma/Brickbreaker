using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] private BrickScoreUI _scorePrefab;
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
        //TELL THE GM OR THE BOARD MANAGER TO INCREASE SCORE
        BrickScoreUI scoreInstance = Instantiate(_scorePrefab);
        scoreInstance.SetScore(_data.Score);
    }

    private void TakeHit()
    {
        _hitsTaken++;
        if(_hitsTaken == _data.HitsToDestroy)
        {
            /*
            Instantiate(_data.DestructionParticleSystem);

            float powerupChance = UnityEngine.Random.Range(0f, 1f);
            if(powerupChance <= _data.PowerupSpawnChance)
            {
                Instantiate(_data.PowerUpToSpawn);
            }
            */
            //BoardManager.instance.DecreaseBricks();
            Destroy(gameObject);
        }
    }
}
