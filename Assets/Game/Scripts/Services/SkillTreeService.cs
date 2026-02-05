using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using StbData;
using UnityEngine;

namespace Services {
public class SkillTreeService : MonoBehaviour {
    public static SkillTreeService Instance;

    public event Action<SkillTreeData> TreeCreated;
    public event Action<string> Exported;

    private SkillTreeData _data;

    private void Awake() {
        Instance = this;
    }

    public void CreateNewTree() {
        var firstNodeData = new SkillTreeNodeData() {
            Id = 0, Position = Vector2.zero, Data = new()
        };
        _data = new SkillTreeData() {
            Id = "new_tree",
            Nodes = new List<SkillTreeNodeData>() { firstNodeData },
            AnchorPosition = Vector2.zero,
            Connectors = new List<SkillTreeConnectorData>()
        };
        TreeCreated?.Invoke(_data);
    }

    public void LoadTree() {
        throw new NotImplementedException();
    }

    public void ExportTree() {
        var settings = new JsonSerializerSettings
        {
            Converters = new List<JsonConverter> { new Vector2Converter() },
            Formatting = Formatting.Indented
        };
        var json = JsonConvert.SerializeObject(_data, settings);
        Exported?.Invoke(json);
    }
}
}