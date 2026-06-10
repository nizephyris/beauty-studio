using TMPro;
using UnityEngine;

public class ResultsParticipantRowView : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private GameObject _winnerIcon;

    public void Bind(string displayName, float averageScore, bool isWinner)
    {
        _nameText.text = displayName;
        _scoreText.text = averageScore.ToString("0.0");
        _winnerIcon.SetActive(isWinner);
    }
}
