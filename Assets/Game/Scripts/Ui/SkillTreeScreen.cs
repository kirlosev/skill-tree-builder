using System;
using StbData;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Ui {
public class SkillTreeScreen : UiScreen, IBeginDragHandler, IDragHandler, IEndDragHandler {
    public static SkillTreeScreen Instance;

    public event Action<Vector2> StartedDrag;
    public event Action<Vector2> EndedDrag;

    [SerializeField] private SkillTreeView _skillTree;
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _friction = 15f;

    private RectTransform _skillTreeRect;
    private Vector2 _treeVelocity;

    private void Awake() {
        Instance = this;
        _skillTreeRect = (RectTransform)_skillTree.transform;
    }

    protected override void TurnOnOffByDefault() {
        TurnOn();
    }

    private void Update() {
        _skillTreeRect.anchoredPosition += _treeVelocity * (_moveSpeed * Time.unscaledDeltaTime);
        _treeVelocity += -_treeVelocity * (_friction * Time.unscaledDeltaTime);
    }

    public void OnBeginDrag(PointerEventData eventData) {
        _treeVelocity = Vector3.zero;
        StartedDrag?.Invoke(eventData.position);
    }

    public void OnDrag(PointerEventData eventData) {
        _treeVelocity += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData) {
        EndedDrag?.Invoke(eventData.position);
    }

    public SkillTreeNodeView GetNodeOnPosition(Vector2 position) {
        return _skillTree.GetNodeOnPosition(position);
    }

    public void ResetTreePosition() {
        _skillTreeRect.anchoredPosition = Vector2.zero;
        _treeVelocity = Vector2.zero;
    }

    public void ZoomIn() {
        var nextScale = Content.localScale.x + 0.25f;
        Content.localScale = new Vector3(nextScale, nextScale, nextScale);
    }

    public void ZoomOut() {
        var nextScale = Content.localScale.x - 0.25f;
        if (nextScale <= 0f) {
            return;
        }
        Content.localScale = new Vector3(nextScale, nextScale, nextScale);
    }
}
}