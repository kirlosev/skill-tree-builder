using Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui {
public class JsonOutputScreen : UiScreen {
    [SerializeField] private TMP_Text _jsonText;
    [SerializeField] private Button _closeButton;

    protected override void TurnOnOffByDefault() {
        TurnOff();
    }

    private void Awake() {
        _closeButton.onClick.AddListener(OnCloseClicked);
    }

    protected override void Start() {
        base.Start();
        SkillTreeService.Instance.Exported += OnExportDataReady;
    }

    private void OnDestroy() {
        SkillTreeService.Instance.Exported -= OnExportDataReady;
    }

    private void OnExportDataReady(string jsonString) {
        _jsonText.SetText(jsonString);
        TurnOn();
    }

    private void OnCloseClicked() {
        TurnOff();
    }
}
}