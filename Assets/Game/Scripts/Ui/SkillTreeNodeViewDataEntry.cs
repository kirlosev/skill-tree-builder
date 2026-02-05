using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui {
public class SkillTreeNodeViewDataEntry : MonoBehaviour {
    public event Action<SkillTreeNodeViewDataEntry> Deleted;
    public event Action<SkillTreeNodeViewDataEntry> Updated;

    [SerializeField] private Button _deleteButton;
    [SerializeField] private TMP_InputField _keyInput;
    [SerializeField] private TMP_InputField _valueInput;

    public (string, string) KeyValue => (_keyInput.text, _valueInput.text);

    private void Awake() {
        _deleteButton.onClick.AddListener(OnDeleteClicked);
        _keyInput.onSubmit.AddListener(OnKeyValueChanged);
        _valueInput.onSubmit.AddListener(OnKeyValueChanged);
    }

    private void OnDeleteClicked() {
        Deleted?.Invoke(this);
        Destroy(gameObject);
    }

    private void OnKeyValueChanged(string _) {
        Updated?.Invoke(this);
    }

    public void Setup(string key, string value) {
        _keyInput.SetTextWithoutNotify(key);
        _valueInput.SetTextWithoutNotify(value);
    }
}
}