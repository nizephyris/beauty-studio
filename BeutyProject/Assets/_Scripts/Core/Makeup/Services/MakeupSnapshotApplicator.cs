using UnityEngine;

public class MakeupSnapshotApplicator
{
    private readonly MakeupCatalogConfig _catalog;

    public MakeupSnapshotApplicator(MakeupCatalogConfig catalog) => _catalog = catalog;

    public Color Apply(CharacterMakeupView view, BotMakeupSnapshot snapshot)
    {
        for (int stageIndex = 0; stageIndex < _catalog.Stages.Count; stageIndex++)
        {
            int optionIndex = snapshot.OptionIndicesByStage[stageIndex];

            if (optionIndex == BotMakeupSnapshot.NoSelectionOptionIndex)
            {
                continue;
            }

            MakeupStageEntry stage = _catalog.Stages[stageIndex];
            MakeupColorOption option = stage.ColorOptions[optionIndex];
            view.ApplyColorOption(stage.Type, option);
        }

        return _catalog.GetBackgroundColorFromSnapshot(snapshot);
    }
}
