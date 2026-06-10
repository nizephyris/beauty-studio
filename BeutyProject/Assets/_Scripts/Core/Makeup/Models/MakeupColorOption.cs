using System;
using UnityEngine;

[Serializable]
public class MakeupColorOption
{
    [SerializeField] private Sprite _overlaySprite;
    [SerializeField] private Color _swatchColor = Color.white;
    [SerializeField] private Color _backgroundColor = Color.white;
    [SerializeField] private HairLengthType _hairLength = HairLengthType.ShortHair;

    public Sprite OverlaySprite => _overlaySprite;

    public Color SwatchColor => _swatchColor;

    public Color BackgroundColor => _backgroundColor;

    public HairLengthType HairLength => _hairLength;
}
