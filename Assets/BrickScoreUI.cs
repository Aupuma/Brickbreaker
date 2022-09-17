using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BrickScoreUI : MonoBehaviour
{
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

        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
