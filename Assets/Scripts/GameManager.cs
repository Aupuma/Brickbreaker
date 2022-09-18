using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private BoardManager _boardManager;
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private BallsManager _ballsManager; 
    [SerializeField] private LimitDetector _limitDetector;
    [SerializeField] private Paddle _paddle;

    [SerializeField] private int initialLives;

    private int _score;
    private int _lives;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _boardManager.BricksDestroyed += BoardManager_BricksDestroyed;
       // _limitDetector.BallLost += LimitDetector_BallLost;

        PrepareGame();
    }

    private void PrepareGame()
    {
        _boardManager.SpawnBoard();
        _paddle.ResetPosition();
        _uiManager.StartCoundown();

        _uiManager.CountdownFinished += StartGame;
    }

    private void StartGame()
    {
        _uiManager.CountdownFinished -= StartGame;
        _ballsManager.CreateBall();
        _paddle.IsInputEnabled = true;
    }



    private void BoardManager_BricksDestroyed()
    {
        _ballsManager.DestroyAllBalls();
        _paddle.IsInputEnabled = false;
        _uiManager.ShowLevelCompletedUI();

        _uiManager.LevelCompletedUIShown += IncreaseLevel;
    }

    private void IncreaseLevel()
    {
        _uiManager.LevelCompletedUIShown -= IncreaseLevel;

        _ballsManager.IncreaseBallGlobalSpeed();
        _paddle.IncreaseSpeed();
        PrepareGame();
    }

    private void LoseLife()
    {
        _lives--;
        if (_lives == 0)
            _uiManager.ShowGameOverUI();
        //RESTART LEVEL
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
        _uiManager.LevelCompletedUIShown -= IncreaseLevel;
        _uiManager.CountdownFinished -= StartGame;
        _boardManager.BricksDestroyed -= BoardManager_BricksDestroyed;
     //   _limitDetector.BallLost -= LimitDetector_BallLost;
    }
}
