using System.Collections.Generic;
using UnityEngine;

public class MakeupFlowService
{
    private readonly MakeupCatalogConfig _catalog;
    private readonly MakeupPlayerLook _playerLook;
    private readonly MakeupScreenView _screen;
    private readonly RatingScreenView _ratingScreen;
    private readonly PlayPageView _playPage;
    private readonly PlayerWalletService _wallet;
    private readonly List<MakeupColorSwatchView> _spawnedSwatches = new();

    private int _currentStageIndex;
    private bool _makeupTimerStarted;

    public MakeupFlowService(MakeupCatalogConfig catalog, MakeupPlayerLook playerLook, MakeupScreenView screen, RatingScreenView ratingScreen, PlayPageView playPage, PlayerWalletService wallet)
    {
        _catalog = catalog;
        _playerLook = playerLook;
        _screen = screen;
        _ratingScreen = ratingScreen;
        _playPage = playPage;
        _wallet = wallet;
        _screen.Bind(this);
    }

    public void StartFlow()
    {
        _wallet.SpendMakeupEnergy();
        ResetMakeupProgress();
        _screen.PlayerCharacterView.RestoreDefaultAppearance();
        ApplyRandomTheme();
        _screen.SetTimerDisplay(_catalog.MakeupDurationSeconds);
        ShowCurrentStage();
    }

    public void ExitToMainPage()
    {
        _screen.StopTimer();
        ResetMakeupProgress();
        _screen.PlayerCharacterView.RestoreDefaultAppearance();
        _wallet.RefundMakeupEnergy();
        _screen.Hide();
        _playPage.Show();
    }

    public void OnStageSelected(int stageIndex)
    {
        DismissThemePlaceholder();
        _currentStageIndex = stageIndex;
        ShowCurrentStage();
    }

    public void CompleteMakeup()
    {
        if (!_playerLook.HasAnySelection)
        {
            return;
        }

        _screen.SetConfirmVisible(true);
    }

    public void ConfirmMakeup() => FinishMakeup();

    public void CancelConfirmMakeup() => _screen.SetConfirmVisible(false);

    public void DismissThemePlaceholder()
    {
        if (!_screen.IsThemePlaceholderVisible)
        {
            return;
        }

        _screen.SetThemePlaceholderVisible(false);
        StartMakeupTimerIfNeeded();
    }

    private void ApplyRandomTheme()
    {
        MakeupThemeEntry theme = _catalog.MakeupThemes[Random.Range(0, _catalog.MakeupThemes.Count)];
        _screen.SetParticipantName(theme.Title);
        _screen.SetThemePlaceholder(theme.Title, theme.Description);
        _screen.SetThemePlaceholderVisible(true);
    }

    private void StartMakeupTimerIfNeeded()
    {
        if (_makeupTimerStarted)
        {
            return;
        }

        _makeupTimerStarted = true;
        _screen.StartTimer(_catalog.MakeupDurationSeconds, FinishMakeup);
    }

    private void FinishMakeup()
    {
        _screen.StopTimer();
        _screen.SetConfirmVisible(false);
        _screen.Hide();
        _ratingScreen.Show();
    }

    private void ResetMakeupProgress()
    {
        ClearSwatches();
        _currentStageIndex = 0;
        _playerLook.Clear();
        _makeupTimerStarted = false;
        _screen.SetThemePlaceholderVisible(false);
        _screen.SetCompleteButtonLocked(true);
        _screen.SetConfirmVisible(false);
        _screen.SetBackgroundColor(_catalog.DefaultBackgroundColor);
    }

    private void ShowCurrentStage()
    {
        MakeupStageEntry stage = _catalog.Stages[_currentStageIndex];
        _screen.StageIndicatorView.SetActiveStage(_currentStageIndex);
        SpawnColorSwatches(stage);
    }

    private void SpawnColorSwatches(MakeupStageEntry stage)
    {
        ClearSwatches();

        IReadOnlyList<MakeupColorOption> options = stage.ColorOptions;

        for (int index = 0; index < options.Count; index++)
        {
            MakeupColorSwatchView swatch = Object.Instantiate(_screen.SwatchPrefab, _screen.SwatchContainer);
            swatch.Bind(options[index], index, OnColorSelected);
            _spawnedSwatches.Add(swatch);
        }
    }

    private void ClearSwatches()
    {
        foreach (MakeupColorSwatchView swatch in _spawnedSwatches)
        {
            Object.Destroy(swatch.gameObject);
        }

        _spawnedSwatches.Clear();
    }

    private void OnColorSelected(int optionIndex)
    {
        DismissThemePlaceholder();
        MakeupStageEntry stage = _catalog.Stages[_currentStageIndex];
        MakeupColorOption option = stage.ColorOptions[optionIndex];
        bool wasFirstSelection = !_playerLook.HasAnySelection;
        ApplySelection(stage.Type, option);
        _playerLook.RecordSelection(stage.Type, optionIndex);

        if (wasFirstSelection)
        {
            _screen.SetCompleteButtonLocked(false);
        }
    }

    private void ApplySelection(MakeupType type, MakeupColorOption option)
    {
        _screen.PlayerCharacterView.ApplyColorOption(type, option);

        if (type == MakeupType.Hair)
        {
            _screen.SetBackgroundColor(option.BackgroundColor);
        }
    }
}
