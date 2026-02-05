using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Scripts.Ui {
public class SkillTreeScreen : UiScreen, IBeginDragHandler, IDragHandler, IEndDragHandler {
    [SerializeField] private RectTransform _skillTreeRect;
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _friction = 15f;

    private Vector2 _treeVelocity;

    protected override void TurnOnOffByDefault() {
        TurnOn();
    }

    private void Update() {
        _skillTreeRect.anchoredPosition += _treeVelocity * (_moveSpeed * Time.unscaledDeltaTime);
        _treeVelocity += -_treeVelocity * (_friction * Time.unscaledDeltaTime);
    }

    public void OnBeginDrag(PointerEventData eventData) {
        _treeVelocity = Vector3.zero;
    }

    public void OnDrag(PointerEventData eventData) {
        _treeVelocity += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData) {
    }
}
}