using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MakeupScreenView : MonoBehaviour
{
    [SerializeField] private CharacterMakeupView _playerCharacterView;
    [SerializeField] private TMP_Text _participantNameText;
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private GameObject _themePlaceholder;
    [SerializeField] private TMP_Text _themeTitleText;
    [SerializeField] private TMP_Text _themeDescriptionText;
    [SerializeField] private MakeupStageIndicatorView _stageIndicatorView;
    [SerializeField] private Transform _swatchContainer;
    [SerializeField] private MakeupColorSwatchView _swatchPrefab;
    [SerializeField] private GameObject _completeButtonBlocker;
    [SerializeField] private GameObject _completeButton;
    [SerializeField] private GameObject _confirmWindow;
    [SerializeField] private Image _backgroundImage;

    private MakeupFlowService _flowService;
    private Action _onTimerExpired;
    private float _timerRemainingSeconds;
    private int _lastDisplayedTimerSeconds = -1;
    private bool _timerRunning;

    public CharacterMakeupView PlayerCharacterView => _playerCharacterView;

    public MakeupStageIndicatorView StageIndicatorView => _stageIndicatorView;

    public Transform SwatchContainer => _swatchContainer;

    public MakeupColorSwatchView SwatchPrefab => _swatchPrefab;

    public GameObject CompleteButtonBlocker => _completeButtonBlocker;

    public GameObject CompleteButton => _completeButton;

    public GameObject ConfirmWindow => _confirmWindow;

    public void Bind(MakeupFlowService flowService)
    {
        _flowService = flowService;
        _stageIndicatorView.Bind(_flowService.OnStageSelected);
    }

    public void SetParticipantName(string name) => _participantNameText.text = name;

    public void SetThemePlaceholder(string title, string description)
    {
        _themeTitleText.text = title;
        _themeDescriptionText.text = description;
    }

    public void SetThemePlaceholderVisible(bool visible) => _themePlaceholder.SetActive(visible);

    public bool IsThemePlaceholderVisible => _themePlaceholder.activeSelf;

    public void DismissThemePlaceholder() => _flowService.DismissThemePlaceholder();

    public void SetTimerDisplay(int durationSeconds)
    {
        _timerRemainingSeconds = durationSeconds;
        _lastDisplayedTimerSeconds = -1;
        _timerRunning = false;
        RefreshTimerText();
    }

    public void StartTimer(int durationSeconds, Action onExpired)
    {
        _onTimerExpired = onExpired;
        _timerRemainingSeconds = durationSeconds;
        _lastDisplayedTimerSeconds = -1;
        _timerRunning = true;
        RefreshTimerText();
    }

    public void StopTimer() => _timerRunning = false;

    public void Show() => gameObject.SetActive(true);

    public void Hide()
    {
        StopTimer();
        gameObject.SetActive(false);
    }

    public void SetConfirmVisible(bool visible) => _confirmWindow.SetActive(visible);

    public void SetCompleteButtonLocked(bool locked)
    {
        _completeButtonBlocker.SetActive(locked);
        _completeButton.SetActive(!locked);
    }

    public void SetBackgroundColor(Color color) => _backgroundImage.color = color;

    private void OnEnable() => _flowService.StartFlow();

    private void Update()
    {
        if (!_timerRunning)
        {
            return;
        }

        _timerRemainingSeconds -= Time.deltaTime;

        if (_timerRemainingSeconds <= 0f)
        {
            _timerRemainingSeconds = 0f;
            _timerRunning = false;
            RefreshTimerText();
            _onTimerExpired();
            return;
        }

        RefreshTimerText();
    }

    private void RefreshTimerText()
    {
        int totalSeconds = Mathf.CeilToInt(_timerRemainingSeconds);

        if (totalSeconds == _lastDisplayedTimerSeconds)
        {
            return;
        }

        _lastDisplayedTimerSeconds = totalSeconds;
        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;
        _timerText.text = $"Осталось {minutes}:{seconds:D2}";
    }
}
