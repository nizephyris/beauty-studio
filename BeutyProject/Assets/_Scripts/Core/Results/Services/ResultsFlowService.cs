using System.Collections.Generic;
using UnityEngine;

public class ResultsFlowService
{
    private readonly ResultsScreenView _screen;
    private readonly RatingScreenView _ratingScreen;
    private readonly RatingSessionModel _session;
    private readonly PlayerWalletService _wallet;
    private readonly CouponShopFlowService _couponShop;
    private readonly List<ResultsParticipantRowView> _spawnedRows = new();

    private bool _rewardPanelSeenThisSession;

    public ResultsFlowService(ResultsScreenView screen, RatingScreenView ratingScreen, RatingSessionModel session, PlayerWalletService wallet, CouponShopFlowService couponShop)
    {
        _screen = screen;
        _ratingScreen = ratingScreen;
        _session = session;
        _wallet = wallet;
        _couponShop = couponShop;
    }

    public void ShowResults()
    {
        _wallet.ResetVictoryRewardState();
        _couponShop.ResetSession();
        _couponShop.CloseShop();
        _ratingScreen.Hide();
        ClearRows();

        IReadOnlyList<LeaderboardEntry> entries = _session.GetLeaderboard();

        for (int index = 0; index < entries.Count; index++)
        {
            LeaderboardEntry entry = entries[index];
            ResultsParticipantRowView row = Object.Instantiate(_screen.ParticipantRowPrefab, _screen.LeaderboardContainer);
            row.Bind(entry.DisplayName, entry.AverageScore, entry.IsWinner);
            _spawnedRows.Add(row);
        }

        _screen.SetVictoryTitle();
        _screen.SetCoinsRewardText(_screen.CoinsRewardLabel);
        _screen.SetLipsRewardText(_screen.LipsRewardLabel);
        _screen.SetRewardPanelVisible(false);
        _screen.Show();
    }

    public void ClaimReward()
    {
        _wallet.GrantVictoryReward();

        if (_rewardPanelSeenThisSession)
        {
            OpenCouponShop();
            return;
        }

        _screen.SetRewardPanelVisible(true);
    }

    public void OpenCouponShopFromResults()
    {
        _rewardPanelSeenThisSession = true;
        OpenCouponShop();
    }

    public void OpenCouponShopFromPlayPage()
    {
        _wallet.RefreshDisplay();
        _couponShop.PrepareAndOpenShop();
    }

    public void CloseCouponShop() => _couponShop.CloseShop();

    private void OpenCouponShop()
    {
        _screen.SetRewardPanelVisible(false);
        _screen.Hide();
        _couponShop.PrepareAndOpenShop();
    }

    private void ClearRows()
    {
        foreach (ResultsParticipantRowView row in _spawnedRows)
        {
            Object.Destroy(row.gameObject);
        }

        _spawnedRows.Clear();
    }
}
