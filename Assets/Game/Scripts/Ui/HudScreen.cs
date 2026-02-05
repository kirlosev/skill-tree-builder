using Services;
using UnityEngine;
using UnityEngine.UI;

namespace Ui {
public class HudScreen : UiScreen {
    [SerializeField] private Button _createTreeButton;
    [SerializeField] private Button _exportButton;

    protected override void TurnOnOffByDefault() {
        TurnOn();
    }

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