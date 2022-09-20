using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpPickUp : Pickup
{
    [SerializeField] PowerUpEffect _effectToSpawn;

    public override void Collect()
    {
        Instantiate(_effectToSpawn);
    }
}
