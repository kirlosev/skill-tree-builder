using System;
using System.Collections.Generic;
using StbData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui {
public class SkillTreeNodeWindow : MonoBehaviour {
    public event Action<int, SkillTreeNodeWindow> Closed;

    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _position;
    [SerializeField] private RectTransform _dataEntriesHolder;
    [SerializeField] private SkillTreeNodeViewDataEntry _dataEntryPrefab;
    [SerializeField] private Button _addDataLineButton;

    private Window _window;
    private SkillTreeNodeData _data;
    private Dictionary<string, SkillTreeNodeViewDataEntry> _lines = new();

    private void Awake() {
        _window = GetComponent<Window>();
        _window.Closed += OnClosed;
        _addDataLineButton.onClick.AddListener(OnAddDataLineClicked);
    }

    private void OnClosed() {
        Closed?.Invoke(_data.Id, this);
    }

    private void OnAddDataLineClicked() {
        const string defaultKey = "new_data_key";
        const string defaultValue = "new_data_value";
        if (_data.Data.ContainsKey(defaultKey)) {
            return;
        }
        _data.Data.Add(defaultKey, defaultValue);
        SpawnLine(defaultKey, defaultValue);
    }

    public void Setup(SkillTreeNodeData data) {
        _data = data;

        _title.SetText($"Node {_data.Id}");
        _position.SetText($"{_data.Position.x},{_data.Position.y}");
        foreach (var d in _data.Data) {
            if (_lines.TryGetValue(d.Key, out var line)) {
                line.Setup(d.Key, d.Value);
                continue;
            }

            SpawnLine(d.Key, d.Value);
        }

        _window.BringToTop();
    }

    private void SpawnLine(string key, string value) {
        var line = Instantiate(_dataEntryPrefab, _dataEntriesHolder);
        line.Setup(key, value);
        line.Deleted += OnDataLineDeleted;
        line.Updated += OnDataLineUpdated;
        _lines.Add(key, line);
    }

    private void OnDataLineDeleted(SkillTreeNodeViewDataEntry line) {
        var (key, value) = line.KeyValue;
        _data.Data.Remove(key);
        _lines.Remove(key);
    }

    private void OnDataLineUpdated(SkillTreeNodeViewDataEntry line) {
        var (key, value) = line.KeyValue;
        _data.Data[key] = value;
    }
}
}