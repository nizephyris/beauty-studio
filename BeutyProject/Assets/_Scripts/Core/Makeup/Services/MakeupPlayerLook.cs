using System.Collections.Generic;

public class MakeupPlayerLook
{
    private readonly Dictionary<MakeupType, int> _optionIndexByType = new();

    public void RecordSelection(MakeupType type, int optionIndex) => _optionIndexByType[type] = optionIndex;

    public bool HasSelection(MakeupType type) => _optionIndexByType.ContainsKey(type);

    public bool HasAnySelection => _optionIndexByType.Count > 0;

    public int GetOptionIndex(MakeupType type) => _optionIndexByType[type];

    public void Clear() => _optionIndexByType.Clear();
}
