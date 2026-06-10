using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MakeupColorSwatchView : MonoBehaviour
{
    [SerializeField] private Image _swatchImage;

    private Button _button;
    private int _optionIndex;
    private Action<int> _onClicked;

    private void Awake() => _button = GetComponent<Button>();

    private void OnEnable() => _button.onClick.AddListener(OnButtonClicked);

    private void OnDisable() => _button.onClick.RemoveListener(OnButtonClicked);

    public void Bind(MakeupColorOption option, int optionIndex, Action<int> onClicked)
    {
        _optionIndex = optionIndex;
        _onClicked = onClicked;
        _swatchImage.color = option.SwatchColor;
        gameObject.SetActive(true);
    }

    private void OnButtonClicked() => _onClicked(_optionIndex);
}
