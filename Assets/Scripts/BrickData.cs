using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Brick Data",menuName ="ScriptableObjects/BrickData")]
public class BrickData : ScriptableObject
{
    public GameObject DestructionParticleSystem;
    public PowerUp PowerUpToSpawn;

    [Range(0,1)] public float PowerupSpawnChance;
    public int HitsToDestroy;
    public int Score;
}
