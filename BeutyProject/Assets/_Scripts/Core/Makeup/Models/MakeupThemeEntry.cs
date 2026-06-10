using System;
using UnityEngine;

[Serializable]
public class MakeupThemeEntry
{
    [SerializeField] private string _title;
    [SerializeField] private string _description;

    public string Title => _title;

    public string Description => _description;
}
