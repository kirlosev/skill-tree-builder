using StbData;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Ui {
public class SkillTreeNodeView : MonoBehaviour, IPointerClickHandler {
    public SkillTreeNodeData Data { get; private set; }

    public Vector2 Pos => ((RectTransform)transform).anchoredPosition;

    public void Setup(SkillTreeNodeData data) {
        Data = data;
    }

    public void OnPointerClick(PointerEventData eventData) {
        WindowsScreen.Instance.ShowNodeWindow(this);
    }
}
}