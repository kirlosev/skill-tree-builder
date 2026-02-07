using Plugins;
using Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui {
public class JsonOutputScreen : UiScreen {
    [SerializeField] private TMP_Text _jsonText;
    [SerializeField] private Button _copyToClipboardButton;
    [SerializeField] private Button _closeButton;

    private string _exportJson;

    protected override void TurnOnOffByDefault() {
        TurnOff();
    }

    private void Awake() {
        _copyToClipboardButton.onClick.AddListener(OnCopyToClipboardClicked);
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
        _exportJson = jsonString;
        _jsonText.SetText(_exportJson);
        TurnOn();
    }

    private void OnCopyToClipboardClicked() {
        Clipboard.CopyToClipboard(_exportJson);
    }

    private void OnCloseClicked() {
        TurnOff();
    }
}
}