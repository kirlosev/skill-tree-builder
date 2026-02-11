using System;
using Services;
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
        var text = data.Id.ToString();
        if (data.Data.TryGetValue("title", out var title)) {
            text = title;
        }
        _idText.SetText(text);
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (_isDragged || LinkToolService.Instance.IsEnabled) {
            return;
        }

        if (DeleteToolService.Instance.IsEnabled) {
            SkillTreeService.Instance.DeleteNode(this);
            return;
        }

        WindowsScreen.Instance.ShowNodeWindow(this);
    }

    public void OnBeginDrag(PointerEventData eventData) {
        if (DeleteToolService.Instance.IsEnabled) {
            return;
        }

        if (LinkToolService.Instance.IsEnabled) {
            SkillTreeScreen.Instance.OnBeginDrag(eventData);
            return;
        }

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
        if (LinkToolService.Instance.IsEnabled || DeleteToolService.Instance.IsEnabled) {
            return;
        }

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _parent,
            eventData.position,
            Camera.main,
            out var pos
        );
        pos += _offset;
        var gs = SkillTreeService.Instance.GetGridSize();
        pos.x = Mathf.Round(pos.x / gs) * gs;
        pos.y = Mathf.Round(pos.y / gs) * gs;
        _rect.anchoredPosition = pos;
    }

    public void OnEndDrag(PointerEventData eventData) {
        if (DeleteToolService.Instance.IsEnabled) {
            return;
        }

        if (LinkToolService.Instance.IsEnabled) {
            SkillTreeScreen.Instance.OnEndDrag(eventData);
            return;
        }

        Data.Position = _rect.anchoredPosition;
        _isDragged = false;
    }

    public bool ContainsPosition(Vector2 position) {
        return RectTransformUtility.RectangleContainsScreenPoint(
            _rect, position, Camera.main
        );
    }
}
}