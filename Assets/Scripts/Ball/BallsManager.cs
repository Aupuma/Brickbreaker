using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsManager : MonoBehaviour
{
    public static BallsManager instance;

    [SerializeField] private Ball _ballPrefab;
    [SerializeField] private Transform _ballSpawnTransform;
    private List<Ball> _balls;

    public event Action AllBallsLost;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _balls = new List<Ball>();
        ResetBallGlobalSpeed();
    }

    public void CreateBall()
    {
        Ball ballInstance = Instantiate(_ballPrefab, _ballSpawnTransform.position, Quaternion.identity);
        _balls.Add(ballInstance);
    }

    public void AddBalls(int amount)
    {
        Debug.Log("Addballs");
        for (int j = 0; j < amount; j++)
        {
            Ball ballInstance = Instantiate(_ballPrefab, _balls[0].transform.position, Quaternion.identity);
            _balls.Add(ballInstance);
        }
    }

    public void LoseBall(Ball ball)
    {
        DestroyBall(ball);
        if(_balls.Count == 0)
        {
            AllBallsLost?.Invoke();
        }
    }

    public void SetBallsOnFire(bool active)
    {
        foreach (var ball in _balls)
        {
            ball.SetFireballActive(active);
        }
    }

    public void DestroyAllBalls()
    {
        while (_balls.Count > 0)
        {
            Ball ballInstance = _balls[_balls.Count - 1];
            DestroyBall(ballInstance);
        }
    }

    private void DestroyBall(Ball ball)
    {
        _balls.Remove(ball);
        ball.Explode();
    }

    public void IncreaseBallGlobalSpeed()
    {
        BallData.Speed += BallData.SpeedLevelIncrease;
    }

    public void ResetBallGlobalSpeed()
    {
        BallData.Speed = BallData.InitialSpeed;
    }
}
