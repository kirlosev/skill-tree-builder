using Services;
using StbData;
using TMPro;
using UnityEngine;

namespace Ui {
public class SnapSizeSelector : MonoBehaviour {
    [SerializeField] private TMP_InputField _valueInput;

    private SkillTreeData _treeData;

    private void Awake() {
        _valueInput.onDeselect.AddListener(OnValueChanged);
    }

    private void OnValueChanged(string _) {
        if (_treeData == null) {
            return;
        }

        if (int.TryParse(_valueInput.text, out var newGridSize)) {
            _treeData.GridSize = newGridSize;
        }
    }

    private void Start() {
        SkillTreeService.Instance.TreeCreated += OnTreeCreated;
    }

    private void OnDestroy() {
        SkillTreeService.Instance.TreeCreated -= OnTreeCreated;
    }

    private void OnTreeCreated(SkillTreeData treeData) {
        _treeData = treeData;
        _valueInput.SetTextWithoutNotify(_treeData.GridSize.ToString());
    }
}
}