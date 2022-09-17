using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreMarkerLabel;


    public void ShowLevelCompletedUI()
    {

    }

    public void UpdateScoreMarker(int score)
    {
        _scoreMarkerLabel.text = score.ToString();
    }

    public void ShowGameOverUI()
    {

    }
}
