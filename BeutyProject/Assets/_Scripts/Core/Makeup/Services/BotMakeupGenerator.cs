using UnityEngine;

public class BotMakeupGenerator
{
    private readonly MakeupCatalogConfig _catalog;
    private readonly MakeupPlayerLook _playerLook;
    private readonly BotRatingRules _rules;

    public BotMakeupGenerator(MakeupCatalogConfig catalog, MakeupPlayerLook playerLook, BotRatingRules rules)
    {
        _catalog = catalog;
        _playerLook = playerLook;
        _rules = rules;
    }

    public BotMakeupSnapshot CreateSnapshot(BotMakeupSnapshot previousBotSnapshot)
    {
        for (int attempt = 0; attempt < 100; attempt++)
        {
            int[] optionIndices = CreateRandomOptionIndices();

            if (IsValidSnapshot(optionIndices, previousBotSnapshot))
            {
                return new BotMakeupSnapshot(optionIndices);
            }
        }

        return new BotMakeupSnapshot(CreateAdjustedOptionIndices(previousBotSnapshot));
    }

    private int[] CreateRandomOptionIndices()
    {
        int stageCount = _catalog.Stages.Count;
        int[] optionIndices = new int[stageCount];

        for (int stageIndex = 0; stageIndex < stageCount; stageIndex++)
        {
            MakeupStageEntry stage = _catalog.Stages[stageIndex];
            optionIndices[stageIndex] = Random.Range(0, stage.ColorOptions.Count);
        }

        return optionIndices;
    }

    private bool IsValidSnapshot(int[] optionIndices, BotMakeupSnapshot previousBotSnapshot)
    {
        if (HasAnyPlayerSelections() && CountDifferencesFromPlayer(optionIndices) < _rules.MinDifferencesFromPlayer)
        {
            return false;
        }

        if (previousBotSnapshot == null)
        {
            return true;
        }

        return CountDifferences(optionIndices, previousBotSnapshot.OptionIndicesByStage) >= _rules.MinDifferencesFromPreviousBot;
    }

    private int[] CreateAdjustedOptionIndices(BotMakeupSnapshot previousBotSnapshot)
    {
        int[] optionIndices = CreateRandomOptionIndices();
        ForceDifferenceFromPlayer(optionIndices);

        if (previousBotSnapshot != null)
        {
            ForceDifferenceFromSnapshot(optionIndices, previousBotSnapshot.OptionIndicesByStage, _rules.MinDifferencesFromPreviousBot);
        }

        return optionIndices;
    }

    private void ForceDifferenceFromPlayer(int[] optionIndices)
    {
        int required = _rules.MinDifferencesFromPlayer - CountDifferencesFromPlayer(optionIndices);

        if (required <= 0)
        {
            return;
        }

        ForceDifferenceFromPlayer(optionIndices, required);
    }

    private void ForceDifferenceFromPlayer(int[] optionIndices, int requiredDifferences)
    {
        int changed = 0;

        for (int stageIndex = 0; stageIndex < _catalog.Stages.Count; stageIndex++)
        {
            if (changed >= requiredDifferences)
            {
                return;
            }

            MakeupStageEntry stage = _catalog.Stages[stageIndex];

            if (!_playerLook.HasSelection(stage.Type))
            {
                continue;
            }

            if (optionIndices[stageIndex] == _playerLook.GetOptionIndex(stage.Type))
            {
                optionIndices[stageIndex] = PickAnotherOptionIndex(stage, optionIndices[stageIndex]);
                changed++;
            }
        }
    }

    private void ForceDifferenceFromSnapshot(int[] optionIndices, int[] sourceIndices, int requiredDifferences)
    {
        int changed = 0;

        for (int stageIndex = 0; stageIndex < _catalog.Stages.Count; stageIndex++)
        {
            if (changed >= requiredDifferences)
            {
                return;
            }

            if (optionIndices[stageIndex] == sourceIndices[stageIndex])
            {
                MakeupStageEntry stage = _catalog.Stages[stageIndex];
                optionIndices[stageIndex] = PickAnotherOptionIndex(stage, optionIndices[stageIndex]);
                changed++;
            }
        }
    }

    private int PickAnotherOptionIndex(MakeupStageEntry stage, int currentIndex)
    {
        if (stage.ColorOptions.Count == 1)
        {
            return currentIndex;
        }

        int nextIndex = Random.Range(0, stage.ColorOptions.Count - 1);

        if (nextIndex >= currentIndex)
        {
            nextIndex++;
        }

        return nextIndex;
    }

    private bool HasAnyPlayerSelections()
    {
        for (int stageIndex = 0; stageIndex < _catalog.Stages.Count; stageIndex++)
        {
            MakeupType type = _catalog.Stages[stageIndex].Type;

            if (_playerLook.HasSelection(type))
            {
                return true;
            }
        }

        return false;
    }

    private int CountDifferencesFromPlayer(int[] optionIndices)
    {
        int differences = 0;

        for (int stageIndex = 0; stageIndex < _catalog.Stages.Count; stageIndex++)
        {
            MakeupStageEntry stage = _catalog.Stages[stageIndex];

            if (!_playerLook.HasSelection(stage.Type))
            {
                continue;
            }

            if (optionIndices[stageIndex] != _playerLook.GetOptionIndex(stage.Type))
            {
                differences++;
            }
        }

        return differences;
    }

    private int CountDifferences(int[] leftOptionIndices, int[] rightOptionIndices)
    {
        int differences = 0;

        for (int stageIndex = 0; stageIndex < _catalog.Stages.Count; stageIndex++)
        {
            if (leftOptionIndices[stageIndex] != rightOptionIndices[stageIndex])
            {
                differences++;
            }
        }

        return differences;
    }
}
