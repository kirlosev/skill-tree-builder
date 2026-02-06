using StbData;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Ui {
public class SkillTreeNodeView : MonoBehaviour, IPointerClickHandler {
    [SerializeField] private TMP_Text _idText;

    public SkillTreeNodeData Data { get; private set; }

    public Vector2 Pos => ((RectTransform)transform).anchoredPosition;

    public void Setup(SkillTreeNodeData data) {
        Data = data;
        _idText.SetText(data.Id.ToString());
    }

    public void OnPointerClick(PointerEventData eventData) {
        WindowsScreen.Instance.ShowNodeWindow(this);
    }
}
}