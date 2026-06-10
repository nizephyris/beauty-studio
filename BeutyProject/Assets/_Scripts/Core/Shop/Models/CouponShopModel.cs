using System.Collections.Generic;

public class CouponShopModel
{
    private readonly HashSet<int> _purchasedCatalogIndices = new();

    public bool IsPurchased(int catalogIndex) => _purchasedCatalogIndices.Contains(catalogIndex);

    public void MarkPurchased(int catalogIndex) => _purchasedCatalogIndices.Add(catalogIndex);

    public void ResetPurchases() => _purchasedCatalogIndices.Clear();
}
