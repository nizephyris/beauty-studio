using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PopupOutsideClickCloser : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private RectTransform _contentZone;
    [SerializeField] private UnityEvent _onOutsideClicked;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(_contentZone, eventData.position, eventData.pressEventCamera))
        {
            return;
        }

        _onOutsideClicked.Invoke();
    }
}
