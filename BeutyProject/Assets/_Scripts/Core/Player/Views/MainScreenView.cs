using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainScreenView : MonoBehaviour
{
    [SerializeField] private TMP_Text _coinsBalanceText;
    [SerializeField] private TMP_Text _lipsBalanceText;
    [SerializeField] private TMP_Text _coinsBalanceTextShop;
    [SerializeField] private TMP_Text _lipsBalanceTextShop;
    [SerializeField] private TMP_Text _energyValueText;
    [SerializeField] private Image _energyFillImage;

    public TMP_Text EnergyValueText => _energyValueText;

    public Image EnergyFillImage => _energyFillImage;

    public void SetCoinsBalance(int coins)
    {
        string balanceText = coins.ToString();
        _coinsBalanceText.text = balanceText;
        _coinsBalanceTextShop.text = balanceText;
    }

    public void SetLipsBalance(int lips)
    {
        string balanceText = lips.ToString();
        _lipsBalanceText.text = balanceText;
        _lipsBalanceTextShop.text = balanceText;
    }
}
