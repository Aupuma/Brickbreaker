using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreMarkerLabel;
    [SerializeField] private TextMeshProUGUI _countDownUI;
    [SerializeField] private TextMeshProUGUI _levelCompletedUI;

    public event Action LevelCompletedUIShown;
    public event Action GameOverUIShown;
    public event Action CountdownFinished;

    public void StartCoundown()
    {
        _countDownUI.gameObject.SetActive(true);
        StartCoroutine(PlayCountdown());
    }

    private IEnumerator PlayCountdown()
    {
   
        _countDownUI.text = "3";
        yield return FadeOutText(_countDownUI,Color.white, 1f);

        _countDownUI.text = "2";
        yield return FadeOutText(_countDownUI, Color.white, 1f);

        _countDownUI.text = "1";
        yield return FadeOutText(_countDownUI, Color.white, 1f);

        _countDownUI.text = "GO";
        yield return FadeOutText(_countDownUI, Color.white, 1f);

        _countDownUI.gameObject.SetActive(false);
        CountdownFinished?.Invoke();
    }

    public void ShowLevelCompletedUI()
    {
        _levelCompletedUI.gameObject.SetActive(true);
        StartCoroutine(PlayLevelCompletedUICoroutine());
    }

    private IEnumerator PlayLevelCompletedUICoroutine()
    {
        yield return FadeOutText(_levelCompletedUI, Color.white, 1f);
        _levelCompletedUI.gameObject.SetActive(false);
        LevelCompletedUIShown?.Invoke();
    }

    public void UpdateScoreMarker(int score)
    {
        _scoreMarkerLabel.text = score.ToString();
    }

    public void ResetScoreMarker()
    {
        _scoreMarkerLabel.text = "0";
    }

    public void ShowGameOverUI()
    {

    }

    private IEnumerator FadeOutText(TextMeshProUGUI textLabel,Color startColor, float duration)
    {
        for (float t = 0f; t <= 1f; t += Time.deltaTime / duration)
        {
            textLabel.color = Color.Lerp(startColor, Color.clear, t);
            yield return null;
        }
    }
}
