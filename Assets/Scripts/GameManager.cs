using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private BoardManager boardManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private LimitDetector _limitDetector;
    [SerializeField] private Ball _ballPrefab;
    [SerializeField] private Transform _ballSpawnTransform;

    private int _currentBalls;
    private int _lives;


    // Start is called before the first frame update
    void Start()
    {
        _limitDetector.BallLost += LimitDetector_BallLost;
    }

    private void LimitDetector_BallLost()
    {
        _currentBalls--;
        if(_currentBalls == 0)
        {
            uiManager.ShowGameOverUI();
            //RESTART LEVEL
        }
    }

    private void SpawnBall()
    {

    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        _limitDetector.BallLost -= LimitDetector_BallLost;
    }
}
