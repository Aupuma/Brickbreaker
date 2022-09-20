using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockLimitEffect : PowerUpEffect
{
    protected override void EndEffect()
    {
        GameManager.instance.SetLimitActive(true);
    }

    protected override void StartEffect()
    {
        GameManager.instance.SetLimitActive(false);
    }
}
