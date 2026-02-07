using Services;
using UnityEngine;

namespace Ui {
public class LinkToolButton : ToggleButton {
    protected override bool IsOn => LinkToolService.Instance.IsEnabled;
    protected override KeyCode Hotkey => KeyCode.C;

    protected override void Start() {
        base.Start();
        LinkToolService.Instance.Changed += OnToolChanged;
    }

    protected override void OnDestroy() {
        base.OnDestroy();
        LinkToolService.Instance.Changed -= OnToolChanged;
    }

    private void OnToolChanged() {
        Refresh();
    }

    protected override void OnClicked() {
        ToolboxService.Instance.ToggleLinkTool();
    }
}
}