using StbData;
using UnityEngine;

namespace Ui {
public class SkillTreeLinkView : MonoBehaviour {
    [SerializeField] private RectTransform _line;

    private RectTransform _parent;
    private RectTransform _rect;
    private SkillTreeLinkData _data;
    private SkillTreeNodeView _fromView;
    private SkillTreeNodeView _toView;

    private void Awake() {
        _parent = (RectTransform)transform.parent;
        _rect = (RectTransform)transform;
    }

    public void Setup(SkillTreeLinkData data, SkillTreeNodeView fromView, SkillTreeNodeView toView) {
        _data = data;
        _fromView = fromView;
        _toView = toView;
    }

    private void Update() {
        if (_fromView == null || _toView == null) {
            return;
        }

        var fromLocalPos = _fromView.Pos;
        var toLocalPos = _toView.Pos;
        var dir = toLocalPos - fromLocalPos;
        _rect.anchoredPosition = fromLocalPos;
        _line.sizeDelta = new Vector2(dir.magnitude, _line.sizeDelta.y);
        _rect.right = dir.normalized;
    }
}
}