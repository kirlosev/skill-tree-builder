using Services;
using UnityEngine;

namespace Ui {
public class DeleteToolButton : ToggleButton {
    protected override bool IsOn => DeleteToolService.Instance.IsEnabled;
    protected override KeyCode Hotkey => KeyCode.D;

    protected override void Start() {
        base.Start();
        DeleteToolService.Instance.Changed += OnToolChanged;
    }

    protected override void OnDestroy() {
        base.OnDestroy();
        DeleteToolService.Instance.Changed -= OnToolChanged;
    }

    private void OnToolChanged() {
        Refresh();
    }

    protected override void OnClicked() {
        ToolboxService.Instance.ToggleDeleteTool();
    }
}
}