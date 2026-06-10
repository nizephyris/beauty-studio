using TMPro;
using UnityEngine;

public class CouponShopScreenView : MonoBehaviour
{
    [SerializeField] private TMP_Text _lipsBalanceText;
    [SerializeField] private Transform _couponContainer;
    [SerializeField] private CouponItemView _couponItemPrefab;

    private CouponShopFlowService _flowService;

    public Transform CouponContainer => _couponContainer;

    public CouponItemView CouponItemPrefab => _couponItemPrefab;

    public void Bind(CouponShopFlowService flowService) => _flowService = flowService;

    public void Show() => gameObject.SetActive(true);

    public void Hide() => gameObject.SetActive(false);

    public void SetLipsBalance(int lips) => _lipsBalanceText.text = lips.ToString();

    private void OnEnable() => _flowService.RefreshShop();
}
