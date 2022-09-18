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

    [SerializeField] private int _initialLives;

    private int _score;
    private int _lives;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _boardManager.BricksDestroyed += LevelComplete;

        _uiManager.GameOverUIShown += ResetGame;

        _limitDetector.BallLost += _ballsManager.LoseBall;
        _ballsManager.AllBallsLost += LoseLife;

        Invoke("ResetGame", 1f);
    }

    private void ResetGame()
    {
        _lives = _initialLives;
        _uiManager.RefillLivesUI(_initialLives);

        _score = 0;
        _uiManager.UpdateScoreMarker(0);

        PrepareGame();
    }

    private void PrepareGame()
    {
        _boardManager.SpawnBoard();
        _paddle.ResetPosition();
        _uiManager.StartCountdown();

        _uiManager.CountdownFinished += StartGame;
    }

    private void StartGame()
    {
        _uiManager.CountdownFinished -= StartGame;

        _ballsManager.CreateBall();
        _paddle.IsInputEnabled = true;
    }

    private void LevelComplete()
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

    public void AddScore(int score)
    {
        _score += score;
        _uiManager.UpdateScoreMarker(_score);
    }

    public void LoseLife()
    {
        _lives--;
        _uiManager.RemoveLifeUI();

        if(_lives == 0)
        {
            FinishGame();
        }
        else
        {
            _ballsManager.CreateBall();
        }
    }

    private void FinishGame()
    {
        _paddle.IsInputEnabled = false;
        _uiManager.ShowGameOverUI();
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        _uiManager.LevelCompletedUIShown -= IncreaseLevel;
        _uiManager.CountdownFinished -= StartGame;
        _uiManager.GameOverUIShown -= ResetGame;
        _boardManager.BricksDestroyed -= LevelComplete;
        
        _limitDetector.BallLost -= _ballsManager.LoseBall;
        _ballsManager.AllBallsLost -= LoseLife;
    }
}
