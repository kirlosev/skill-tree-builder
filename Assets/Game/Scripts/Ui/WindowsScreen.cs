using System.Collections.Generic;
using Services;
using StbData;
using UnityEngine;

namespace Ui {
public class WindowsScreen : UiScreen {
    public static WindowsScreen Instance;

    [SerializeField] private RectTransform _windowsHolder;
    [SerializeField] private SkillTreeNodeWindow _nodeWindowPrefab;
    [SerializeField] private NodeDataDefaultsWindow _dataDefaultsWindowPrefab;

    private Dictionary<int, SkillTreeNodeWindow> _nodeWindows = new();
    private NodeDataDefaultsWindow _dataDefaultsWindow;

    private void Awake() {
        Instance = this;
    }

    protected virtual void Start() {
        base.Start();
        SkillTreeService.Instance.TreeCreated += OnTreeCreated;
    }

    private void OnDestroy() {
        SkillTreeService.Instance.TreeCreated -= OnTreeCreated;
    }

    private void OnTreeCreated(SkillTreeData data) {
        CloseAllWindows();
    }

    private void CloseAllWindows() {
        foreach (var (id, view) in _nodeWindows) {
            Destroy(view.gameObject);
        }
        _nodeWindows.Clear();
    }

    protected override void TurnOnOffByDefault() {
        TurnOn();
    }

    public void ShowNodeWindow(SkillTreeNodeView nodeView) {
        var data = nodeView.Data;
        var links = SkillTreeService.Instance.GetNodeLinks(data.Id);
        if (_nodeWindows.TryGetValue(data.Id, out var window)) {
            window.Setup(data, links);
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
        window.Setup(data, links);
        _nodeWindows.Add(data.Id, window);
    }

    private void OnNodeWindowClosed(int id, SkillTreeNodeWindow nodeWindow) {
        _nodeWindows.Remove(id);
    }

    public void ShowDataDefaultsWindow() {
        if (_dataDefaultsWindow != null) {
            _dataDefaultsWindow.Setup();
            return;
        }

        _dataDefaultsWindow = Instantiate(_dataDefaultsWindowPrefab, _windowsHolder);
        _dataDefaultsWindow.Closed += OnDataDefaultsWindowClosed;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _windowsHolder,
            Input.mousePosition,
            Camera.main,
            out var localPos
        );
        ((RectTransform)_dataDefaultsWindow.transform).anchoredPosition = localPos;
        _dataDefaultsWindow.Setup();
    }

    private void OnDataDefaultsWindowClosed(NodeDataDefaultsWindow _) {
        _dataDefaultsWindow.Closed -= OnDataDefaultsWindowClosed;
        _dataDefaultsWindow = null;
    }
}
}