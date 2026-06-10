public class BotMakeupSnapshot
{
    public const int NoSelectionOptionIndex = -1;

    public BotMakeupSnapshot(int[] optionIndicesByStage) => OptionIndicesByStage = optionIndicesByStage;

    public int[] OptionIndicesByStage { get; }
}
