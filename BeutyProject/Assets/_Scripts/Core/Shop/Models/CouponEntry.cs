using System;
using UnityEngine;

[Serializable]
public class CouponEntry
{
    [SerializeField] private string _type;
    [SerializeField] private string _mainText;
    [SerializeField] private string _additionalText;
    [SerializeField] private int _price;

    public string Type => _type;

    public string MainText => _mainText;

    public string AdditionalText => _additionalText;

    public int PriceInLips => _price;
}
