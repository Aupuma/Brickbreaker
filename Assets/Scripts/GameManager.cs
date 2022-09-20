using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private BrickManager _bricksManager;
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
        _bricksManager.BricksDestroyed += CompleteLevel;

        _uiManager.GameOverUIShown += ResetGame;

        _limitDetector.BallLost += _ballsManager.LoseBall;
        _ballsManager.AllBallsLost += LoseLife;

        Invoke("ResetGame", 1f);
    }

    private void OnDestroy()
    {
        _uiManager.LevelCompletedUIShown -= IncreaseLevel;
        _uiManager.CountdownFinished -= StartGame;
        _uiManager.GameOverUIShown -= ResetGame;
        _bricksManager.BricksDestroyed -= CompleteLevel;

        _limitDetector.BallLost -= _ballsManager.LoseBall;
        _ballsManager.AllBallsLost -= LoseLife;
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

        if (_lives == 0)
        {
            FinishGame();
        }
        else
        {
            _ballsManager.CreateBall();
        }
    }

    public void AddBalls(int amount)
    {
        _ballsManager.AddBalls(amount);
    }

    public void SetLimitActive(bool active)
    {
        _limitDetector.SetLimitActive(active);
    }

    private void ResetGame()
    {
        _lives = _initialLives;
        _uiManager.RefillLivesUI(_initialLives);

        _score = 0;
        _uiManager.UpdateScoreMarker(0);

        _ballsManager.ResetBallGlobalSpeed();
        _paddle.ResetSpeed();

        PrepareGame();
    }

    private void PrepareGame()
    {
        _bricksManager.SpawnBoard();
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

    private void CompleteLevel()
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

    private void FinishGame()
    {
        _paddle.IsInputEnabled = false;
        _uiManager.ShowGameOverUI();
    }
}
