using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CouponItemView : MonoBehaviour
{
    [SerializeField] private TMP_Text _typeText;
    [SerializeField] private TMP_Text _mainText;
    [SerializeField] private TMP_Text _additionalText;
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private Button _purchaseButton;
    [SerializeField] private RectTransform rootObjectRect;
    [SerializeField] private float popUpDuration = 0.5f;

    private int _catalogIndex;
    private CouponShopFlowService _shop;

    public int CatalogIndex => _catalogIndex;

    public void SetCanAfford(bool canAfford) => _purchaseButton.interactable = canAfford;

    public void Bind(int catalogIndex, CouponEntry entry, CouponShopFlowService shop)
    {
        _catalogIndex = catalogIndex;
        _shop = shop;
        _typeText.text = entry.Type;
        _mainText.text = entry.MainText;
        _additionalText.text = entry.AdditionalText;
        _priceText.text = entry.PriceInLips.ToString();
        _purchaseButton.interactable = shop.CanAffordCoupon(catalogIndex);
        _purchaseButton.onClick.RemoveListener(OnPurchaseButtonClicked);
        _purchaseButton.onClick.AddListener(OnPurchaseButtonClicked);
    }

    private void OnPurchaseButtonClicked()
    {
        bool purchased = _shop.TryPurchaseCoupon(_catalogIndex);

        if (!purchased)
        {
            return;
        }

        _purchaseButton.interactable = false;
        _shop.RemovePurchasedCouponView(_catalogIndex);
        rootObjectRect.DOScale(0f, popUpDuration)
            .SetEase(Ease.OutCubic)
            .OnComplete(() => Destroy(gameObject));
    }
}
