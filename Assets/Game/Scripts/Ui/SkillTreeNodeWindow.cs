using StbData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui {
public class SkillTreeNodeWindow : MonoBehaviour {
    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _position;
    [SerializeField] private RectTransform _dataEntriesHolder;
    [SerializeField] private SkillTreeNodeViewDataEntry _dataEntryPrefab;
    [SerializeField] private Button _addDataLineButton;

    private SkillTreeNodeData _data;

    private void Awake() {
        _addDataLineButton.onClick.AddListener(OnAddDataLineClicked);
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
            SpawnLine(d.Key, d.Value);
        }
    }

    private void SpawnLine(string key, string value) {
        var line = Instantiate(_dataEntryPrefab, _dataEntriesHolder);
        line.Setup(key, value);
        line.Deleted += OnDataLineDeleted;
        line.Updated += OnDataLineUpdated;
    }

    private void OnDataLineDeleted(SkillTreeNodeViewDataEntry line) {
        var (key, value) = line.KeyValue;
        _data.Data.Remove(key);
    }

    private void OnDataLineUpdated(SkillTreeNodeViewDataEntry line) {
        var (key, value) = line.KeyValue;
        _data.Data[key] = value;
    }
}
}