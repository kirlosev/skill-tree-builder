using System;
using Ui;
using UnityEngine;

namespace Services {
public class LinkToolService : MonoBehaviour {
    public static LinkToolService Instance;

    public event Action Changed;
    public event Action StartedLinking;
    public event Action StoppedLinking;

    private SkillTreeNodeView _fromNode;

    public bool IsEnabled { get; private set; }

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        SkillTreeScreen.Instance.StartedDrag += OnStartedDrag;
        SkillTreeScreen.Instance.EndedDrag += OnEndedDrag;
    }

    private void OnDestroy() {
        SkillTreeScreen.Instance.StartedDrag -= OnStartedDrag;
        SkillTreeScreen.Instance.EndedDrag -= OnEndedDrag;
    }

    private void OnStartedDrag(Vector2 screenPos) {
        if (!IsEnabled) {
            return;
        }

        var nodeView = SkillTreeScreen.Instance.GetNodeOnPosition(screenPos);
        _fromNode = nodeView;
        StartedLinking?.Invoke();
    }

    private void OnEndedDrag(Vector2 screenPos) {
        if (!IsEnabled) {
            return;
        }

        var nodeView = SkillTreeScreen.Instance.GetNodeOnPosition(screenPos);
        if (nodeView != null) {
            SkillTreeService.Instance.LinkNodes(_fromNode, nodeView);
        }
        StoppedLinking?.Invoke();
        _fromNode = null;
    }

    public void ToggleLinkTool() {
        IsEnabled = !IsEnabled;
        Changed?.Invoke();
    }
}
}