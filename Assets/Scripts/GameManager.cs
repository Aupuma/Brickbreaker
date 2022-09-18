using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private BoardManager _boardManager;
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private LimitDetector _limitDetector;
    [SerializeField] private Paddle _paddle;
    [SerializeField] private Ball _ballPrefab;
    [SerializeField] private Transform _ballSpawnTransform;

    [SerializeField] private int initialLives;

    private int _score;
    private int _lives;

    private List<Ball> _balls;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _balls = new List<Ball>();
        _boardManager.BricksDestroyed += BoardManager_BricksDestroyed;
        _limitDetector.BallLost += LimitDetector_BallLost;

        PrepareGame();
    }

    private void PrepareGame()
    {
        _boardManager.SpawnBoard();
        _uiManager.StartCoundown();
        _uiManager.CountdownFinished += StartGame;
    }

    private void StartGame()
    {
        _uiManager.CountdownFinished -= StartGame;
        CreateBall();
        _paddle.IsInputEnabled = true;
    }

    private void CreateBall()
    {
        Ball ballInstance = Instantiate(_ballPrefab, _ballSpawnTransform.position, Quaternion.identity);
        _balls.Add(ballInstance);
    }

    private void DestroyAllBalls()
    {
        while(_balls.Count > 0)
        {
            Ball ballInstance = _balls[_balls.Count - 1];
            _balls.Remove(ballInstance);
            ballInstance.Explode();
        }
    }

    private void BoardManager_BricksDestroyed()
    {
        DestroyAllBalls();
        _paddle.IsInputEnabled = false;

        _uiManager.ShowLevelCompletedUI();
        _uiManager.LevelCompletedUIShown += UIManager_LevelCompletedUIShown;
    }

    private void UIManager_LevelCompletedUIShown()
    {
        _uiManager.LevelCompletedUIShown -= UIManager_LevelCompletedUIShown;
        PrepareGame();
    }

    private void LimitDetector_BallLost(Ball ball)
    {
        _balls.Remove(ball);
        ball.Explode();

        if(_balls.Count == 0)
        {
            LoseLife();
        }
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
        _uiManager.LevelCompletedUIShown -= UIManager_LevelCompletedUIShown;
        _uiManager.CountdownFinished -= StartGame;
        _boardManager.BricksDestroyed -= BoardManager_BricksDestroyed;
        _limitDetector.BallLost -= LimitDetector_BallLost;
    }
}
