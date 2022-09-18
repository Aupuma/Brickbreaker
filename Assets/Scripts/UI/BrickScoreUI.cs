using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BrickScoreUI : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private float _heightToMove;
    private TextMeshPro _scoreLabel; 

    private void Awake()
    {
        _scoreLabel = GetComponent<TextMeshPro>();
    }

    public void SetScore(int score)
    {
        _scoreLabel.text = "+" + score;
        StartCoroutine(ShowScoreCoroutine());
    }

    private IEnumerator ShowScoreCoroutine()
    {
        Vector3 initialPosition = transform.position;
        Vector3 finalPosition = new Vector3(initialPosition.x, initialPosition.y + _heightToMove, initialPosition.z);
        for (float t = 0f; t <= 1f; t += Time.deltaTime/_duration)
        {
            _scoreLabel.color = Color.Lerp(Color.white, Color.clear, t);
            transform.position = Vector3.Lerp(initialPosition, finalPosition, t);
            yield return null;
        }
        Destroy(gameObject);
    }
}
