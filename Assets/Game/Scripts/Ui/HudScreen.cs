using Services;
using UnityEngine;
using UnityEngine.UI;

namespace Ui {
public class HudScreen : UiScreen {
    [SerializeField] private Button _importTreeButton;
    [SerializeField] private Button _newTreeButton;
    [SerializeField] private Button _addNodeButton;
    [SerializeField] private Button _resetTreePositionButton;
    [SerializeField] private Button _exportButton;
    [SerializeField] private Button _editDataDefaultsButton;

    protected override void TurnOnOffByDefault() {
        TurnOn();
    }

    private void Awake() {
        _importTreeButton.onClick.AddListener(OnImportTreeClicked);
        _newTreeButton.onClick.AddListener(OnNewTreeClicked);
        _addNodeButton.onClick.AddListener(OnAddNodeClicked);
        _resetTreePositionButton.onClick.AddListener(OnResetTreePositionClicked);
        _exportButton.onClick.AddListener(OnExportClicked);
        _editDataDefaultsButton.onClick.AddListener(OnEditDataDefaultsClicked);
    }

    private void OnImportTreeClicked() {
        ImportTreeScreen.Instance.TurnOn();
    }

    private void OnNewTreeClicked() {
        SkillTreeService.Instance.CreateNewTree();
    }

    private void OnAddNodeClicked() {
        SkillTreeService.Instance.AddNode();
    }

    private void OnResetTreePositionClicked() {
        SkillTreeScreen.Instance.ResetTreePosition();
    }

    private void OnExportClicked() {
        SkillTreeService.Instance.ExportTree();
    }

    private void OnEditDataDefaultsClicked() {
        WindowsScreen.Instance.ShowDataDefaultsWindow();
    }
}
}
