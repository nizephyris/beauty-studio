using UnityEngine;

public class BotRatingFlowService
{
    private readonly RatingScreenView _screen;
    private readonly BotRatingRules _rules;
    private readonly BotMakeupGenerator _botMakeupGenerator;
    private readonly MakeupSnapshotApplicator _snapshotApplicator;
    private readonly RatingSessionModel _session;
    private readonly ResultsFlowService _resultsFlow;

    private BotMakeupSnapshot _previousBotSnapshot;
    private int _currentBotIndex;
    private int _currentRating;

    public BotRatingFlowService(RatingScreenView screen, BotRatingRules rules, BotMakeupGenerator botMakeupGenerator, MakeupSnapshotApplicator snapshotApplicator, RatingSessionModel session, ResultsFlowService resultsFlow)
    {
        _screen = screen;
        _rules = rules;
        _botMakeupGenerator = botMakeupGenerator;
        _snapshotApplicator = snapshotApplicator;
        _session = session;
        _resultsFlow = resultsFlow;
        _screen.Bind(this);
    }

    public void StartFlow()
    {
        _session.Reset();
        _currentBotIndex = 0;
        _previousBotSnapshot = null;
        ShowBotRound();
    }

    public void SetRating(int rating)
    {
        _currentRating = rating;
        _screen.RatingScaleView.SetRating(_currentRating);
        _screen.SetNextButtonEnabled(true);
    }

    public void GoToNextBot()
    {
        if (_currentBotIndex >= _rules.BotCount || _currentRating == 0)
        {
            return;
        }

        _session.RecordBotRating(_currentBotIndex, _currentRating);
        _currentBotIndex++;

        if (_currentBotIndex >= _rules.BotCount)
        {
            _screen.SetNextButtonEnabled(false);
            _resultsFlow.ShowResults();
            return;
        }

        ShowBotRound();
    }

    private void ShowBotRound()
    {
        BotMakeupSnapshot snapshot = _botMakeupGenerator.CreateSnapshot(_previousBotSnapshot);
        Color backgroundColor = _snapshotApplicator.Apply(_screen.BotCharacterView, snapshot);
        _screen.SetBackgroundColor(backgroundColor);
        _session.SetBotMakeupSnapshot(_currentBotIndex, snapshot);
        _previousBotSnapshot = snapshot;
        _currentRating = 0;
        _screen.SetParticipantName(_session.GetBotName(_currentBotIndex));
        _screen.RatingScaleView.SetRating(0);
        _screen.SetNextButtonEnabled(false);
        _screen.SetContinueLabel(_currentBotIndex == _rules.BotCount - 1);
    }
}
