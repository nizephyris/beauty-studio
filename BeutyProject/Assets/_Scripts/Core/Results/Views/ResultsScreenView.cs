using TMPro;
using UnityEngine;

public class ResultsScreenView : MonoBehaviour
{
    [SerializeField] private Transform _leaderboardContainer;
    [SerializeField] private ResultsParticipantRowView _participantRowPrefab;
    [SerializeField] private TMP_Text _victoryTitleText;
    [SerializeField] private string _victoryTitle = "Вы победили!";
    [SerializeField] private TMP_Text _coinsRewardText;
    [SerializeField] private TMP_Text _lipsRewardText;
    [SerializeField] private string _coinsRewardLabel = "+100 баллов";
    [SerializeField] private string _lipsRewardLabel = "+200 липсов";
    [SerializeField] private GameObject _rewardPanel;

    public Transform LeaderboardContainer => _leaderboardContainer;

    public ResultsParticipantRowView ParticipantRowPrefab => _participantRowPrefab;

    public string CoinsRewardLabel => _coinsRewardLabel;

    public string LipsRewardLabel => _lipsRewardLabel;

    public void Show() => gameObject.SetActive(true);

    public void Hide() => gameObject.SetActive(false);

    public void SetVictoryTitle() => _victoryTitleText.text = _victoryTitle;

    public void SetCoinsRewardText(string text) => _coinsRewardText.text = text;

    public void SetLipsRewardText(string text) => _lipsRewardText.text = text;

    public void SetRewardPanelVisible(bool visible) => _rewardPanel.SetActive(visible);
}
