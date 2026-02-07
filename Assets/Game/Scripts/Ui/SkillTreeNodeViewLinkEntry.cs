using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui {
public class SkillTreeNodeViewLinkEntry : MonoBehaviour {
    public event Action<SkillTreeNodeViewLinkEntry> Deleted;

    [SerializeField] private Button _deleteButton;
    [SerializeField] private TMP_InputField _valueInput;

    private int _nodeId;

    private void Awake() {
        _deleteButton.onClick.AddListener(OnDeleteClicked);
    }

    private void OnDeleteClicked() {
        Deleted?.Invoke(this);
        Destroy(gameObject);
    }

    public void Setup(int nodeId) {
        _nodeId = nodeId;
        _valueInput.SetTextWithoutNotify(_nodeId.ToString());
    }
}
}