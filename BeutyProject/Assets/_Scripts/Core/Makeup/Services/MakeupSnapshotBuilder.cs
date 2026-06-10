public class MakeupSnapshotBuilder
{
    private readonly MakeupCatalogConfig _catalog;

    public MakeupSnapshotBuilder(MakeupCatalogConfig catalog) => _catalog = catalog;

    public BotMakeupSnapshot BuildFromPlayerLook(MakeupPlayerLook playerLook)
    {
        int stageCount = _catalog.Stages.Count;
        int[] optionIndices = new int[stageCount];

        for (int stageIndex = 0; stageIndex < stageCount; stageIndex++)
        {
            MakeupStageEntry stage = _catalog.Stages[stageIndex];
            optionIndices[stageIndex] = playerLook.HasSelection(stage.Type)
                ? playerLook.GetOptionIndex(stage.Type)
                : BotMakeupSnapshot.NoSelectionOptionIndex;
        }

        return new BotMakeupSnapshot(optionIndices);
    }
}
