using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager instance;

    [Serializable]
    public struct BoardParameters
    {
        public int Rows;
        public float SpaceBetweenRows;
        public int Columns;
        public float SpaceBetweenColumns;
    }

    [SerializeField] private Transform _boardParent;
    [SerializeField] private Transform _firstSpawnTransform;
    [SerializeField] private Brick[] _brickPrefabs;
    [SerializeField] float _timeBetweenBrickSpawns;
    [SerializeField] private BoardParameters _boardParameters;

    private int _bricksAmount;

    public event Action BricksDestroyed;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //SpawnBoard();
    }

    /// <summary>
    /// Creates a board of bricks. The type of brick is spawned depending on randomness,
    /// with the chance of not spawning any bricks
    /// </summary>
    public void SpawnBoard()
    {
        foreach (var item in _boardParent.GetComponentsInChildren<Brick>())
        {
            DestroyImmediate(item.gameObject);
        }

        for (int i = 0; i < _boardParameters.Columns; i++)
        {
            for (int j = 0; j < _boardParameters.Rows; j++)
            {
                int brickId = GetBrickId();
                Vector3 brickPosition = GetBrickPosition(i, j);

                SpawnBrick(brickId, brickPosition);
            }
        }
    }

    //private IEnumerator SpawnBoardCoroutine()
    //{

    //}

    private void SpawnBrick(int id, Vector3 position)
    {
        if (id == -1)
            return;

        Brick brickInstance = Instantiate(_brickPrefabs[id], position, Quaternion.identity);
        brickInstance.transform.SetParent(_boardParent);
        _bricksAmount++;
    }

    public void DecreaseBricks()
    {
        _bricksAmount--;
        if(_bricksAmount == 0)
        {
            BricksDestroyed?.Invoke();
        }
    }

    private Vector3 GetBrickPosition(int column, int row)
    {
        return new Vector3(_firstSpawnTransform.position.x + column * _boardParameters.SpaceBetweenColumns,
                            _firstSpawnTransform.position.y + row * _boardParameters.SpaceBetweenRows,
                            _firstSpawnTransform.position.z);
    }

    private int GetBrickId()
    {
        return UnityEngine.Random.Range(-1, _brickPrefabs.Length);
    }
}
