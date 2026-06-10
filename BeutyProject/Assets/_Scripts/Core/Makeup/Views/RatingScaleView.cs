using UnityEngine;
using UnityEngine.UI;

public class RatingScaleView : MonoBehaviour
{
    [SerializeField] private Image[] _icons;
    [SerializeField] private Sprite _defaultSprite;
    [SerializeField] private Sprite _chosenSprite;

    public void SetRating(int rating)
    {
        for (int index = 0; index < _icons.Length; index++)
        {
            _icons[index].sprite = index < rating ? _chosenSprite : _defaultSprite;
        }
    }
}
