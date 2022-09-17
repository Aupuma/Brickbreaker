using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager instance;

    [SerializeField] private Transform _boardParent;
    [SerializeField] private Transform _initialSpawnPosition;
    [SerializeField] private int _rows;
    [SerializeField] private float _spaceBetweenRows;

    [SerializeField] private int _columns;
    [SerializeField] private float _spaceBetweenColumns;

    [SerializeField] private Brick[] _brickPrefabs;
    private List<Brick> _levelBricks;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //Initialize bricks list
    }

    /// <summary>
    /// Creates a board of bricks. The type of brick is spawned depending on randomness,
    /// with the chance of not spawning any bricks
    /// </summary>
    public void SpawnBoard()
    {
        for (int i = 0; i < _columns; i++)
        {
            for (int j = 0; j < _rows; j++)
            {
                int brickId = GetBrickId();
                Vector3 brickPosition = GetBrickPosition(i, j);

                SpawnBrick(brickId, brickPosition);
            }
        }
    }

    private void SpawnBrick(int id, Vector3 position)
    {
        //IF ID == -1 LEAVE A GAP, HOW TO MANAGE THIS WITHOUT KNOWING LIST SIZE?
        Brick brickInstance = Instantiate(_brickPrefabs[id], position, Quaternion.identity);
        brickInstance.transform.SetParent(_boardParent);
        _levelBricks.Add(brickInstance);
    }

    private Vector3 GetBrickPosition(int column, int row)
    {
        return new Vector3(_initialSpawnPosition.position.x + column * _spaceBetweenColumns,
                            _initialSpawnPosition.position.y + row * _spaceBetweenRows,
                            _initialSpawnPosition.position.z);
    }

    private int GetBrickId()
    {
        return Random.Range(-1, _brickPrefabs.Length);
    }
}
