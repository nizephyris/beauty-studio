using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class MakeupStageIconSlot
{
    [SerializeField] private Image _icon;
    [SerializeField] private Button _button;
    [SerializeField] private Sprite _defaultSprite;
    [SerializeField] private Sprite _currentSprite;

    public Button Button => _button;

    public void SetDefault() => _icon.sprite = _defaultSprite;

    public void SetCurrent() => _icon.sprite = _currentSprite;
}
