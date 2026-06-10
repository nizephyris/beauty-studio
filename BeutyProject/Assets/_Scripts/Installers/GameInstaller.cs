using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private MakeupScreenView _makeupScreen;
    [SerializeField] private RatingScreenView _ratingScreen;
    [SerializeField] private TutorialScreenView _tutorialScreen;
    [SerializeField] private ResultsScreenView _resultsScreen;
    [SerializeField] private MainScreenView _mainScreen;
    [SerializeField] private PlayPageView _playPage;
    [SerializeField] private CouponShopScreenView _couponShopScreen;
    [SerializeField] private MakeupCatalogConfig _catalog;
    [SerializeField] private BotRatingRules _botRatingRules;
    [SerializeField] private PlayerWalletConfig _playerWalletConfig;

    public override void InstallBindings()
    {
        Container.BindInstance(_makeupScreen);
        Container.BindInstance(_ratingScreen);
        Container.BindInstance(_tutorialScreen);
        Container.BindInstance(_resultsScreen);
        Container.BindInstance(_mainScreen);
        Container.BindInstance(_playPage);
        Container.BindInstance(_couponShopScreen);
        Container.BindInstance(_catalog);
        Container.BindInstance(_botRatingRules);
        Container.BindInstance(_playerWalletConfig);

        Container.Bind<MakeupPlayerLook>().AsSingle();
        Container.Bind<RatingSessionModel>().AsSingle();
        Container.Bind<PlayerWalletModel>().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerWalletService>().AsSingle();
        Container.Bind<CouponShopModel>().AsSingle();
        Container.Bind<CouponShopFlowService>().AsSingle().NonLazy();
        Container.Bind<MakeupSnapshotBuilder>().AsSingle();
        Container.Bind<MakeupSnapshotApplicator>().AsSingle();
        Container.Bind<BotMakeupGenerator>().AsSingle();
        Container.Bind<MakeupFlowService>().AsSingle().NonLazy();
        Container.Bind<BotRatingFlowService>().AsSingle().NonLazy();
        Container.Bind<TutorialFlowService>().AsSingle().NonLazy();
        Container.Bind<ResultsFlowService>().AsSingle();

        Container.Bind<GameUiActions>().FromComponentOn(gameObject).AsSingle();
    }
}
