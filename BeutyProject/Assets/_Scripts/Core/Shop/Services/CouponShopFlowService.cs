using System.Collections.Generic;
using UnityEngine;

public class CouponShopFlowService
{
    private readonly CouponShopScreenView _screen;
    private readonly MakeupCatalogConfig _catalog;
    private readonly CouponShopModel _shopModel;
    private readonly PlayerWalletService _wallet;
    private readonly List<CouponItemView> _spawnedCoupons = new();

    public CouponShopFlowService(CouponShopScreenView screen, MakeupCatalogConfig catalog, CouponShopModel shopModel, PlayerWalletService wallet)
    {
        _screen = screen;
        _catalog = catalog;
        _shopModel = shopModel;
        _wallet = wallet;
        _screen.Bind(this);
    }

    public void ResetSession() => _shopModel.ResetPurchases();

    public void PrepareAndOpenShop()
    {
        ResetSession();
        CloseShop();
        OpenShop();
    }

    public void OpenShop()
    {
        if (_screen.gameObject.activeSelf)
        {
            RefreshShop();
            return;
        }

        _screen.Show();
    }

    public void CloseShop()
    {
        ClearCouponViews();
        _screen.Hide();
    }

    public void RefreshShop()
    {
        ClearCouponViews();
        SpawnCoupons();
        RefreshLipsBalance();
    }

    public void RefreshLipsBalance()
    {
        _screen.SetLipsBalance(_wallet.Lips);
        RefreshCouponAffordability();
    }

    public bool TryPurchaseCoupon(int catalogIndex)
    {
        if (_shopModel.IsPurchased(catalogIndex))
        {
            return false;
        }

        CouponEntry entry = _catalog.Coupons[catalogIndex];

        if (!_wallet.TrySpendLips(entry.PriceInLips))
        {
            return false;
        }

        _shopModel.MarkPurchased(catalogIndex);
        RefreshLipsBalance();
        RefreshCouponAffordability();
        return true;
    }

    public bool CanAffordCoupon(int catalogIndex) =>
        !_shopModel.IsPurchased(catalogIndex) && _wallet.Lips >= _catalog.Coupons[catalogIndex].PriceInLips;

    public void RemovePurchasedCouponView(int catalogIndex)
    {
        for (int index = 0; index < _spawnedCoupons.Count; index++)
        {
            if (_spawnedCoupons[index].CatalogIndex != catalogIndex)
            {
                continue;
            }

            _spawnedCoupons.RemoveAt(index);
            return;
        }
    }

    private void SpawnCoupons()
    {
        IReadOnlyList<CouponEntry> coupons = _catalog.Coupons;

        for (int index = 0; index < coupons.Count; index++)
        {
            if (_shopModel.IsPurchased(index))
            {
                continue;
            }

            CouponItemView couponView = Object.Instantiate(_screen.CouponItemPrefab, _screen.CouponContainer);
            couponView.Bind(index, coupons[index], this);
            _spawnedCoupons.Add(couponView);
        }
    }

    private void RefreshCouponAffordability()
    {
        foreach (CouponItemView couponView in _spawnedCoupons)
        {
            couponView.SetCanAfford(CanAffordCoupon(couponView.CatalogIndex));
        }
    }

    private void ClearCouponViews()
    {
        _spawnedCoupons.Clear();

        Transform container = _screen.CouponContainer;

        for (int childIndex = container.childCount - 1; childIndex >= 0; childIndex--)
        {
            Object.Destroy(container.GetChild(childIndex).gameObject);
        }
    }
}
