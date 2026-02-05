using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Ui {
public class Window : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerDownHandler {
    [SerializeField] private Button _closeButton;

    private RectTransform _rect;
    private RectTransform _parent;
    private Vector2 _offset;

    private void Awake() {
        _closeButton.onClick.AddListener(OnCloseClicked);
        _rect = (RectTransform)transform;
        _parent = (RectTransform)transform.parent;
    }

    private void OnCloseClicked() {
        Destroy(gameObject);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _parent,
            eventData.position,
            Camera.main,
            out var pos
        );
        _offset = _rect.anchoredPosition - pos;
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _parent,
            eventData.position,
            Camera.main,
            out var pos
        );
        pos += _offset;
        pos.x = Mathf.Clamp(pos.x, _rect.sizeDelta.x / 2f, _parent.sizeDelta.x - _rect.sizeDelta.x / 2f);
        pos.y = Mathf.Clamp(pos.y, _rect.sizeDelta.y / 2f, _parent.sizeDelta.y - _rect.sizeDelta.y / 2f);
        _rect.anchoredPosition = pos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _rect.SetAsLastSibling();
    }
}
}