using UnityEngine;

[CreateAssetMenu(fileName = "PlayerWallet", menuName = "FoxWorld/Player Wallet Config")]
public class PlayerWalletConfig : ScriptableObject
{
    [SerializeField] private int _initialCoins;
    [SerializeField] private int _initialLips;
    [SerializeField] private int _initialEnergy = 100;
    [SerializeField] private int _maxEnergy = 100;
    [SerializeField] private int _makeupEnergyCost = 5;
    [SerializeField] private int _victoryRewardCoins = 100;
    [SerializeField] private int _victoryRewardLips = 200;

    public int InitialCoins => _initialCoins;

    public int InitialLips => _initialLips;

    public int InitialEnergy => _initialEnergy;

    public int MaxEnergy => _maxEnergy;

    public int MakeupEnergyCost => _makeupEnergyCost;

    public int VictoryRewardCoins => _victoryRewardCoins;

    public int VictoryRewardLips => _victoryRewardLips;
}
