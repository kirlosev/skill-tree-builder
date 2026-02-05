using Services;
using UnityEngine;
using UnityEngine.UI;

namespace Ui {
public class HudScreen : MonoBehaviour {
    [SerializeField] private Button _createTreeButton;
    [SerializeField] private Button _exportButton;

    private void Awake() {
        _createTreeButton.onClick.AddListener(OnCreateTreeClicked);
        _exportButton.onClick.AddListener(OnExportClicked);
    }

    private void OnCreateTreeClicked() {
        SkillTreeService.Instance.CreateNewTree();
    }

    private void OnExportClicked() {
        SkillTreeService.Instance.ExportTree();
    }
}
}