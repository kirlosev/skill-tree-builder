using StbData;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Ui {
public class SkillTreeNodeView : MonoBehaviour, IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler {
    [SerializeField] private TMP_Text _idText;

    private RectTransform _rect;
    private RectTransform _parent;
    private Vector2 _offset;
    private bool _isDragged;

    public SkillTreeNodeData Data { get; private set; }

    public Vector2 Pos => ((RectTransform)transform).anchoredPosition;

    private void Awake() {
        _rect = (RectTransform)transform;
        _parent = (RectTransform)transform.parent;
    }

    public void Setup(SkillTreeNodeData data) {
        Data = data;
        _idText.SetText(data.Id.ToString());
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (_isDragged) {
            return;
        }

        WindowsScreen.Instance.ShowNodeWindow(this);
    }

    public void OnBeginDrag(PointerEventData eventData) {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _parent,
            eventData.position,
            Camera.main,
            out var pos
        );
        _offset = _rect.anchoredPosition - pos;
        _isDragged = true;
    }

    public void OnDrag(PointerEventData eventData) {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _parent,
            eventData.position,
            Camera.main,
            out var pos
        );
        pos += _offset;
        _rect.anchoredPosition = pos;
    }

    public void OnEndDrag(PointerEventData eventData) {
        Data.Position = _rect.anchoredPosition;
        _isDragged = false;
    }
}
}