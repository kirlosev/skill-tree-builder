using Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui {
public class ImportTreeScreen : UiScreen {
    public static ImportTreeScreen Instance;

    [SerializeField] private Button _importButton;
    [SerializeField] private Button _closeButton;
    [SerializeField] private TMP_InputField _jsonInputField;

    protected override void TurnOnOffByDefault() {
        TurnOff();
    }

    private void Awake() {
        Instance = this;
        _importButton.onClick.AddListener(OnImportClicked);
        _closeButton.onClick.AddListener(OnCloseClicked);
    }

    private void OnCloseClicked() {
        TurnOff();
    }

    private void OnImportClicked() {
        var json = _jsonInputField.text;
        SkillTreeService.Instance.LoadTree(json);
        TurnOff();
    }
}
}