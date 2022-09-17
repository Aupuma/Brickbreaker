using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] private GameObject _scorePrefab;
    [SerializeField] private GameObject _destructionParticleSystem;
    [SerializeField] private GameObject _powerUpToSpawn;

    [SerializeField] private int _hitsToDestroy;
    [SerializeField] private int _score;

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
        Instantiate(_scorePrefab);
    }

    private void TakeHit()
    {
        _hitsTaken++;
        if(_hitsTaken == _hitsToDestroy)
        {
            //SPAWN DESTROY PARTICLE SYSTEM
            //IF LUCKY, SPAWN POWER UP
            //TELL THE BOARD MANAGER THAT BRICK HAS BEEN DESTROYED
        }
    }
}
