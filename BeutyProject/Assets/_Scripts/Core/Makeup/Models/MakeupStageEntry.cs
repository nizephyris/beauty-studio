using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MakeupStageEntry
{
    [SerializeField] private MakeupType _type;
    [SerializeField] private List<MakeupColorOption> _colorOptions;

    public MakeupType Type => _type;

    public IReadOnlyList<MakeupColorOption> ColorOptions => _colorOptions;
}
