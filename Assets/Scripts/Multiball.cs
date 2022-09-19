using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multiball : PowerUp
{
    [SerializeField] private int ballsToAdd;


    protected override void StartEffect()
    {
        GameManager.instance.AddBalls(ballsToAdd);
    }

    protected override void EndEffect()
    {
        //Nothing
    }
}
