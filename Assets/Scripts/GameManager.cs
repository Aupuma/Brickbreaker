using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private BoardManager _boardManager;
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private LimitDetector _limitDetector;

    [SerializeField] private Ball _ballPrefab;
    [SerializeField] private Transform _ballSpawnTransform;

    [SerializeField] private int initialLives;

    private int _score;
    private int _currentBalls;
    private int _lives;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _boardManager.BricksDestroyed += BoardManager_BricksDestroyed;
        _limitDetector.BallLost += LimitDetector_BallLost;
    }

    private void PrepareGame()
    {
        _boardManager.SpawnBoard();
        _uiManager.StartCoundown();
    }

    private void StartGame()
    {
        Ball ballInstance = Instantiate(_ballPrefab, _ballSpawnTransform.position, Quaternion.identity);
        //ENABLE CONTROLS
    }


    private void BoardManager_BricksDestroyed()
    {
        _uiManager.ShowLevelCompletedUI();
    }

    private void LimitDetector_BallLost()
    {
        _currentBalls--;
        if(_currentBalls == 0)
        {
            _uiManager.ShowGameOverUI();
            //RESTART LEVEL
        }
    }

    public void AddScore(int score)
    {
        _score += score;
        _uiManager.UpdateScoreMarker(_score);
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
