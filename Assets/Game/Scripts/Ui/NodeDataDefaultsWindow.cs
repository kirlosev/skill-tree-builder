using System;
using System.Collections.Generic;
using Services;
using StbData;
using UnityEngine;
using UnityEngine.UI;

namespace Ui {
public class NodeDataDefaultsWindow : MonoBehaviour {
    public event Action<NodeDataDefaultsWindow> Closed;

    [SerializeField] private RectTransform _dataEntriesHolder;
    [SerializeField] private SkillTreeNodeViewDataEntry _dataEntryPrefab;
    [SerializeField] private Button _addDataLineButton;

    private Window _window;
    private SkillTreeNodeData _data;
    private List<SkillTreeNodeViewDataEntry> _dataLines = new();

    private void Awake() {
        _window = GetComponent<Window>();
        _window.Closed += OnClosed;
        _addDataLineButton.onClick.AddListener(OnAddDataLineClicked);
    }

    private void OnClosed() {
        Closed?.Invoke(this);
    }

    private void OnAddDataLineClicked() {
        const string defaultKey = "new_data_key";
        const string defaultValue = "new_data_value";
        if (_data.Data.ContainsKey(defaultKey)) {
            return;
        }
        _data.Data.Add(defaultKey, defaultValue);
        SpawnDataLine(defaultKey, defaultValue);
    }

    public void Setup() {
        _data = SkillTreeService.Instance.GetDataDefaults() ?? new SkillTreeNodeData();
        foreach (Transform t in _dataEntriesHolder) {
            Destroy(t.gameObject);
        }
        _dataLines.Clear();

        foreach (var d in _data.Data) {
            SpawnDataLine(d.Key, d.Value);
        }

        _window.BringToTop();
    }

    private void SpawnDataLine(string key, string value) {
        var line = Instantiate(_dataEntryPrefab, _dataEntriesHolder);
        line.Setup(key, value);
        line.Deleted += OnDataLineDeleted;
        line.Updated += OnDataLineUpdated;
        _dataLines.Add(line);
    }

    private void OnDataLineDeleted(SkillTreeNodeViewDataEntry line) {
        var (key, value) = line.KeyValue;
        _data.Data.Remove(key);
        _dataLines.Remove(line);
    }

    private void OnDataLineUpdated(SkillTreeNodeViewDataEntry _) {
        _data.Data.Clear();
        foreach (var line in _dataLines) {
            var (key, value) = line.KeyValue;
            _data.Data[key] = value;
        }
        SkillTreeService.Instance.SetNodeDataDefaults(_data);
    }
}
}