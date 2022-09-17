using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreMarkerLabel;
    [SerializeField] private TextMeshProUGUI _countDownUI;

    public void StartCoundown()
    {
        StartCoroutine(PlayCountdown());
    }

    private IEnumerator PlayCountdown()
    {
        _countDownUI.text = "3";
        yield return new WaitForSeconds(1f);

        _countDownUI.text = "2";
        yield return new WaitForSeconds(1f);

        _countDownUI.text = "1";
        yield return new WaitForSeconds(1f);

        _countDownUI.text = "GO";
        yield return new WaitForSeconds(1f);

        //LET THE GAME BEGIN, WARN THE GAME MANAGER TO START
    }




    public void ShowLevelCompletedUI()
    {

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
}
