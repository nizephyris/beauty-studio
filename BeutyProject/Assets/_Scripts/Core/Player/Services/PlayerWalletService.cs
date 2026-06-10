using Zenject;

public class PlayerWalletService : IInitializable
{
    private readonly PlayerWalletModel _wallet;
    private readonly PlayerWalletConfig _config;
    private readonly MainScreenView _mainScreen;

    private bool _victoryRewardGranted;

    public PlayerWalletService(PlayerWalletModel wallet, PlayerWalletConfig config, MainScreenView mainScreen)
    {
        _wallet = wallet;
        _config = config;
        _mainScreen = mainScreen;

        _wallet.Coins = _config.InitialCoins;
        _wallet.Lips = _config.InitialLips;
        _wallet.Energy = _config.InitialEnergy;
    }

    public int Lips => _wallet.Lips;

    public void Initialize() => RefreshDisplay();

    public void ResetVictoryRewardState() => _victoryRewardGranted = false;

    public void RefreshDisplay() => ApplyDisplay(_wallet.Coins, _wallet.Lips, _wallet.Energy);

    public bool TrySpendLips(int amount)
    {
        if (_wallet.Lips < amount)
        {
            return false;
        }

        _wallet.Lips -= amount;
        _mainScreen.SetLipsBalance(_wallet.Lips);
        return true;
    }

    public void SpendMakeupEnergy()
    {
        int nextEnergy = _wallet.Energy - _config.MakeupEnergyCost;

        if (nextEnergy < 0)
        {
            nextEnergy = 0;
        }

        _wallet.Energy = nextEnergy;
        ApplyEnergyDisplay(_wallet.Energy);
    }

    public void RefundMakeupEnergy()
    {
        int nextEnergy = _wallet.Energy + _config.MakeupEnergyCost;

        if (nextEnergy > _config.MaxEnergy)
        {
            nextEnergy = _config.MaxEnergy;
        }

        _wallet.Energy = nextEnergy;
        ApplyEnergyDisplay(_wallet.Energy);
    }

    public void GrantVictoryReward()
    {
        if (_victoryRewardGranted)
        {
            return;
        }

        _victoryRewardGranted = true;
        _wallet.Coins += _config.VictoryRewardCoins;
        _wallet.Lips += _config.VictoryRewardLips;
        _mainScreen.SetCoinsBalance(_wallet.Coins);
        _mainScreen.SetLipsBalance(_wallet.Lips);
    }

    private void ApplyDisplay(int coins, int lips, int energy)
    {
        _mainScreen.SetCoinsBalance(coins);
        _mainScreen.SetLipsBalance(lips);
        ApplyEnergyDisplay(energy);
    }

    private void ApplyEnergyDisplay(int energy)
    {
        _mainScreen.EnergyValueText.text = energy.ToString();
        _mainScreen.EnergyFillImage.fillAmount = (float)energy / _config.MaxEnergy;
    }
}
