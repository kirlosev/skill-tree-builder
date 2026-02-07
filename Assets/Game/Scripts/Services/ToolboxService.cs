using UnityEngine;

namespace Services {
public interface ITool {
}

public class ToolboxService : MonoBehaviour {
    public static ToolboxService Instance;

    private void Awake() {
        Instance = this;
    }

    public void ToggleLinkTool() {
        LinkToolService.Instance.Toggle();
        if (DeleteToolService.Instance.IsEnabled) {
            DeleteToolService.Instance.Toggle();
        }
    }

    public void ToggleDeleteTool() {
        DeleteToolService.Instance.Toggle();
        if (LinkToolService.Instance.IsEnabled) {
            LinkToolService.Instance.Toggle();
        }
    }
}
}