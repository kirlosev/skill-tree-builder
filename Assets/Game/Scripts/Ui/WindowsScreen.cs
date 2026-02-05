using System.Collections.Generic;
using StbData;
using UnityEngine;

namespace Ui {
public class WindowsScreen : UiScreen {
    public static WindowsScreen Instance;

    [SerializeField] private RectTransform _windowsHolder;
    [SerializeField] private SkillTreeNodeWindow _nodeWindowPrefab;

    private Dictionary<int, SkillTreeNodeWindow> _nodeWindows = new();

    private void Awake() {
        Instance = this;
    }

    protected override void TurnOnOffByDefault() {
        TurnOn();
    }

    public void ShowNodeWindow(SkillTreeNodeView nodeView) {
        var data = nodeView.Data;
        if (_nodeWindows.TryGetValue(data.Id, out var window)) {
            window.Setup(data);
            return;
        }

        window = Instantiate(_nodeWindowPrefab, _windowsHolder);
        window.Closed += OnNodeWindowClosed;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _windowsHolder,
            Input.mousePosition,
            Camera.main,
            out var localPos
        );
        ((RectTransform)window.transform).anchoredPosition = localPos;
        window.Setup(data);
        _nodeWindows.Add(data.Id, window);
    }

    private void OnNodeWindowClosed(int id, SkillTreeNodeWindow nodeWindow) {
        _nodeWindows.Remove(id);
    }
}
}