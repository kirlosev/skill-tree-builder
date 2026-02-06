using System;
using UnityEngine;

namespace Services {
public class ToolboxService : MonoBehaviour {
    public static ToolboxService Instance;

    public event Action Changed;

    public bool IsLinkToolEnabled { get; private set; }

    private void Awake() {
        Instance = this;
    }

    public void ToggleLinkTool() {
        IsLinkToolEnabled = !IsLinkToolEnabled;
        Changed?.Invoke();
    }
}
}