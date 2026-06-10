using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RatingScreenView : MonoBehaviour
{
    [SerializeField] private CharacterMakeupView _botCharacterView;
    [SerializeField] private RatingScaleView _ratingScaleView;
    [SerializeField] private TMP_Text _participantNameText;
    [SerializeField] private GameObject _nextButton;
    [SerializeField] private GameObject _nextButtonBlocker;
    [SerializeField] private TMP_Text _continueButtonText;
    [SerializeField] private string _continueButtonLabel;
    [SerializeField] private string _lastBotContinueButtonLabel;
    [SerializeField] private Image _backgroundImage;

    private BotRatingFlowService _flowService;

    public CharacterMakeupView BotCharacterView => _botCharacterView;

    public RatingScaleView RatingScaleView => _ratingScaleView;

    public void Bind(BotRatingFlowService flowService) => _flowService = flowService;

    public void SetParticipantName(string name) => _participantNameText.text = name;

    public void SetContinueLabel(bool isLastBot) =>
        _continueButtonText.text = isLastBot ? _lastBotContinueButtonLabel : _continueButtonLabel;

    public void SetNextButtonEnabled(bool enabled)
    {
        _nextButton.SetActive(enabled);
        _nextButtonBlocker.SetActive(!enabled);
    }

    public void SetBackgroundColor(Color color) => _backgroundImage.color = color;

    public void Show() => gameObject.SetActive(true);

    public void Hide() => gameObject.SetActive(false);

    private void OnEnable() => _flowService.StartFlow();
}
