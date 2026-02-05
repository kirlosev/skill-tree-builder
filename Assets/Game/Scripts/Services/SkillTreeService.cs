using System;
using System.Collections.Generic;
using StbData;
using UnityEngine;

namespace Services {
public class SkillTreeService : MonoBehaviour {
    public static SkillTreeService Instance;

    public event Action<SkillTreeData> TreeCreated;

    private void Awake() {
        Instance = this;
    }

    public void CreateNewTree() {
        var firstNodeData = new SkillTreeNodeData() {
            Id = 0, Position = Vector2.zero, Data = new()
        };
        var treeData = new SkillTreeData() {
            Id = "new_tree",
            Nodes = new List<SkillTreeNodeData>() { firstNodeData },
            AnchorPosition = Vector2.zero,
            Connectors = new List<SkillTreeConnectorData>()
        };
        TreeCreated?.Invoke(treeData);
    }

    public void LoadTree() {
        throw new NotImplementedException();
    }

    public void ExportTree() {
        throw new NotImplementedException();
    }
}
}