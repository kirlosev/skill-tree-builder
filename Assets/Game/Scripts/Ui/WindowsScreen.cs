using StbData;
using UnityEngine;

namespace Ui {
public class WindowsScreen : UiScreen {
    public static WindowsScreen Instance;

    [SerializeField] private RectTransform _windowsHolder;
    [SerializeField] private SkillTreeNodeWindow _nodeWindowPrefab;

    private void Awake() {
        Instance = this;
    }

    protected override void TurnOnOffByDefault() {
        TurnOn();
    }

    public void ShowNodeWindow(SkillTreeNodeView nodeView) {
        var window = Instantiate(_nodeWindowPrefab, _windowsHolder);

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _windowsHolder,
            Input.mousePosition,
            Camera.main,
            out var localPos
        );
        ((RectTransform)window.transform).anchoredPosition = localPos;
        window.Setup(nodeView.Data);
    }
}
}