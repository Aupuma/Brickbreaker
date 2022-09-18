using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ball Data", menuName = "ScriptableObjects/BallData")]
public class BallData : ScriptableObject
{
    public static float InitialSpeed = 10f;
    public static float SpeedLevelIncrease = 5f;
    public static float Speed;
    public float WaitTime;
    public float MinReflectionAngleNoise;
    public float MaxReflectionAngleNoise;
    public float MaxStartAngleOffset;
}
