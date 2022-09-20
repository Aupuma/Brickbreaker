using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballEffect : PowerUpEffect
{
    protected override void StartEffect()
    {
        BrickManager.instance.SetBricksToTrigger(true);
    }

    protected override void EndEffect()
    {
        BrickManager.instance.SetBricksToTrigger(false);
    }
}
