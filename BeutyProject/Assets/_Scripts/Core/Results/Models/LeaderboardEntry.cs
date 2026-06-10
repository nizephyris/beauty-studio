public class LeaderboardEntry
{
    public LeaderboardEntry(string displayName, float averageScore, bool isWinner)
    {
        DisplayName = displayName;
        AverageScore = averageScore;
        IsWinner = isWinner;
    }

    public string DisplayName { get; }

    public float AverageScore { get; }

    public bool IsWinner { get; }
}
