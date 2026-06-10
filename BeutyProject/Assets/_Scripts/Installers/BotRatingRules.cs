using System;
using UnityEngine;

[Serializable]
public class BotRatingRules
{
    [SerializeField] private int _botCount = 3;
    [SerializeField] private int _minDifferencesFromPlayer = 1;
    [SerializeField] private int _minDifferencesFromPreviousBot = 2;

    public int BotCount => _botCount;

    public int MinDifferencesFromPlayer => _minDifferencesFromPlayer;

    public int MinDifferencesFromPreviousBot => _minDifferencesFromPreviousBot;
}
