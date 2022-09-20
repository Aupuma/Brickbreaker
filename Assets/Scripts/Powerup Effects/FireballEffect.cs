using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballEffect : PowerUpEffect
{
    protected override void StartEffect()
    {
        BallsManager.instance.SetBallsOnFire(true);
        BrickManager.instance.SetBricksToTrigger(true);
    }

    protected override void EndEffect()
    {
        BallsManager.instance.SetBallsOnFire(false);
        BrickManager.instance.SetBricksToTrigger(false);
    }
}
