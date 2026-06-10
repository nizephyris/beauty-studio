using UnityEngine;
using Zenject;

public class GameUiActions : MonoBehaviour
{
    private MakeupFlowService _makeupFlow;
    private BotRatingFlowService _botRatingFlow;
    private ResultsFlowService _resultsFlow;

    [Inject]
    public void Construct(MakeupFlowService makeupFlow, BotRatingFlowService botRatingFlow, ResultsFlowService resultsFlow)
    {
        _makeupFlow = makeupFlow;
        _botRatingFlow = botRatingFlow;
        _resultsFlow = resultsFlow;
    }

    public void CompleteMakeup() => _makeupFlow.CompleteMakeup();

    public void ConfirmMakeup() => _makeupFlow.ConfirmMakeup();

    public void CancelConfirmMakeup() => _makeupFlow.CancelConfirmMakeup();

    public void ExitMakeup() => _makeupFlow.ExitToMainPage();

    public void SetRatingFromUi(int rating) => _botRatingFlow.SetRating(rating);

    public void GoToNextBot() => _botRatingFlow.GoToNextBot();

    public void ClaimReward() => _resultsFlow.ClaimReward();

    public void OpenCouponShopFromResults() => _resultsFlow.OpenCouponShopFromResults();

    public void OpenCouponShopFromPlayPage() => _resultsFlow.OpenCouponShopFromPlayPage();

    public void CloseCouponShop() => _resultsFlow.CloseCouponShop();
}
