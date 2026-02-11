using System;
using System.Collections.Generic;
using Services;
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
    [SerializeField] private RectTransform _linksEntriesHolder;
    [SerializeField] private SkillTreeNodeViewLinkEntry _linkEntryHolder;

    private Window _window;
    private SkillTreeNodeData _data;
    private List<SkillTreeNodeViewDataEntry> _dataLines = new();
    private Dictionary<int, SkillTreeNodeViewLinkEntry> _linkLines = new();

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
        SpawnDataLine(defaultKey, defaultValue);
    }

    public void Setup(SkillTreeNodeData data, List<int> linkedNodesIds) {
        _data = data;

        _title.SetText($"Node {_data.Id}");
        _position.SetText($"{_data.Position.x},{_data.Position.y}");

        foreach (Transform t in _dataEntriesHolder) {
            Destroy(t.gameObject);
        }
        _dataLines.Clear();

        foreach (var d in _data.Data) {
            SpawnDataLine(d.Key, d.Value);
        }

        foreach (var id in linkedNodesIds) {
            if (_linkLines.TryGetValue(id, out var line)) {
                line.Setup(id);
                continue;
            }

            SpawnLinkLine(id);
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
        SkillTreeService.Instance.RaiseNodeDataChanged(_data);
    }

    private void SpawnLinkLine(int id) {
        var line = Instantiate(_linkEntryHolder, _linksEntriesHolder);
        line.Setup(id);
        line.Deleted += OnLinkLineDeleted;
        _linkLines.Add(id, line);
    }

    private void OnLinkLineDeleted(SkillTreeNodeViewLinkEntry line) {
        SkillTreeService.Instance.RemoveLink(_data.Id, line.NodeId);
        _linkLines.Remove(line.NodeId);
    }
}
}