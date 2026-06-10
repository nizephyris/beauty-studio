using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CharacterMakeupView : MonoBehaviour
{
    [SerializeField] private Image _lipsOverlay;
    [SerializeField] private Image _lashesOverlay;
    [FormerlySerializedAs("_shortHairOverlay")]
    [SerializeField] private Image _defaultHairOverlay;
    [SerializeField] private Image _longHairOverlay;

    private Sprite _defaultLipsSprite;
    private Sprite _defaultLashesSprite;
    private Sprite _defaultHairSprite;
    private Sprite _defaultLongHairSprite;
    private bool _defaultLipsEnabled;
    private bool _defaultLashesEnabled;
    private bool _defaultHairEnabled;
    private bool _defaultLongHairEnabled;
    private bool _defaultHairActive;
    private bool _defaultLongHairActive;

    private void Awake()
    {
        _defaultLipsSprite = _lipsOverlay.sprite;
        _defaultLashesSprite = _lashesOverlay.sprite;
        _defaultHairSprite = _defaultHairOverlay.sprite;
        _defaultLongHairSprite = _longHairOverlay.sprite;
        _defaultLipsEnabled = _lipsOverlay.enabled;
        _defaultLashesEnabled = _lashesOverlay.enabled;
        _defaultHairEnabled = _defaultHairOverlay.enabled;
        _defaultLongHairEnabled = _longHairOverlay.enabled;
        _defaultHairActive = _defaultHairOverlay.gameObject.activeSelf;
        _defaultLongHairActive = _longHairOverlay.gameObject.activeSelf;
    }

    public void SetHairOverlay(HairLengthType hairLength, Sprite sprite)
    {
        if (hairLength == HairLengthType.LongHair)
        {
            _defaultHairOverlay.gameObject.SetActive(false);
            _longHairOverlay.gameObject.SetActive(true);
            _longHairOverlay.sprite = sprite;
            _longHairOverlay.enabled = true;
            return;
        }

        _longHairOverlay.gameObject.SetActive(false);
        _defaultHairOverlay.gameObject.SetActive(true);
        _defaultHairOverlay.sprite = sprite;
        _defaultHairOverlay.enabled = true;
    }

    public void ApplyColorOption(MakeupType type, MakeupColorOption option)
    {
        if (type == MakeupType.Hair)
        {
            SetHairOverlay(option.HairLength, option.OverlaySprite);
            return;
        }

        SetOverlay(type, option.OverlaySprite);
    }

    public void SetOverlay(MakeupType type, Sprite sprite)
    {
        Image overlay = GetOverlay(type);
        overlay.sprite = sprite;
        overlay.enabled = true;
    }

    public void RestoreDefaultAppearance()
    {
        _lipsOverlay.sprite = _defaultLipsSprite;
        _lipsOverlay.enabled = _defaultLipsEnabled;
        _lashesOverlay.sprite = _defaultLashesSprite;
        _lashesOverlay.enabled = _defaultLashesEnabled;
        _defaultHairOverlay.sprite = _defaultHairSprite;
        _defaultHairOverlay.enabled = _defaultHairEnabled;
        _defaultHairOverlay.gameObject.SetActive(_defaultHairActive);
        _longHairOverlay.sprite = _defaultLongHairSprite;
        _longHairOverlay.enabled = _defaultLongHairEnabled;
        _longHairOverlay.gameObject.SetActive(_defaultLongHairActive);
    }

    private Image GetOverlay(MakeupType type)
    {
        if (type == MakeupType.Lips)
        {
            return _lipsOverlay;
        }

        return _lashesOverlay;
    }
}
