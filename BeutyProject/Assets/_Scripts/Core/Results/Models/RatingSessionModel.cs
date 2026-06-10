using System.Collections.Generic;
using UnityEngine;

public class RatingSessionModel
{
    private const float PlayerTestScore = 5f;

    private readonly MakeupCatalogConfig _catalog;
    private readonly BotRatingRules _rules;
    private readonly List<string> _botNames = new();
    private readonly List<float> _botScores = new();
    private readonly List<BotMakeupSnapshot> _botMakeupSnapshots = new();

    private string _playerName;

    public RatingSessionModel(MakeupCatalogConfig catalog, BotRatingRules rules)
    {
        _catalog = catalog;
        _rules = rules;
    }

    public string PlayerName => _playerName;

    public void Reset()
    {
        _playerName = _catalog.PlayerDisplayName;
        _botNames.Clear();
        _botScores.Clear();
        _botMakeupSnapshots.Clear();

        List<string> namePool = new List<string>(_catalog.BotDisplayNames);
        ShuffleNames(namePool);

        for (int botIndex = 0; botIndex < _rules.BotCount; botIndex++)
        {
            _botNames.Add(namePool[botIndex % namePool.Count]);
            _botScores.Add(0f);
        }
    }

    public string GetBotName(int botIndex)
    {
        EnsureBotSlot(botIndex);
        return _botNames[botIndex];
    }

    public void SetBotMakeupSnapshot(int botIndex, BotMakeupSnapshot snapshot)
    {
        EnsureBotSlot(botIndex);
        _botMakeupSnapshots.Add(snapshot);
    }

    public void RecordBotRating(int botIndex, int rating)
    {
        EnsureBotSlot(botIndex);
        _botScores[botIndex] = rating;
    }

    public IReadOnlyList<LeaderboardEntry> GetLeaderboard()
    {
        List<LeaderboardEntry> entries = new()
        {
            new LeaderboardEntry(_playerName, PlayerTestScore, false)
        };

        for (int botIndex = 0; botIndex < _botNames.Count; botIndex++)
        {
            entries.Add(new LeaderboardEntry(_botNames[botIndex], _botScores[botIndex], false));
        }

        entries.Sort((left, right) => right.AverageScore.CompareTo(left.AverageScore));

        for (int index = 0; index < entries.Count; index++)
        {
            LeaderboardEntry entry = entries[index];
            entries[index] = new LeaderboardEntry(entry.DisplayName, entry.AverageScore, index == 0);
        }

        return entries;
    }

    private void EnsureBotSlot(int botIndex)
    {
        while (_botNames.Count <= botIndex)
        {
            int slotIndex = _botNames.Count;
            IReadOnlyList<string> botNames = _catalog.BotDisplayNames;
            _botNames.Add(botNames[slotIndex % botNames.Count]);
            _botScores.Add(0f);
        }
    }

    private void ShuffleNames(List<string> names)
    {
        for (int index = names.Count - 1; index > 0; index--)
        {
            int swapIndex = Random.Range(0, index + 1);
            string temporary = names[index];
            names[index] = names[swapIndex];
            names[swapIndex] = temporary;
        }
    }
}
