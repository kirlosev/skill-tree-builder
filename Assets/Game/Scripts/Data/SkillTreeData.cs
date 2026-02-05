using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace StbData {
[Serializable]
public class SkillTreeNode {
    [JsonProperty("id")] public int Id;
    [JsonProperty("position")] public Vector2 Position;
    [JsonProperty("data")] public Dictionary<string, string> Data = new();

    [JsonIgnore] public bool IsUnlocked;

    public SkillTreeNode(bool isUnlocked, List<string> dataFields) {
        IsUnlocked = isUnlocked;
        Data.Clear();
        foreach (var df in dataFields) {
            Data[df] = "";
        }
    }
}

[Serializable]
public class SkillTreeConnector {
    [JsonProperty("from_id")] public int FromNodeId;
    [JsonProperty("to_id")] public int ToNodeId;
    [JsonProperty("is_two_way")] public bool IsTwoWay = true;
}

[Serializable]
public class SkillTreeData {
    [JsonProperty("version")] public int Version;
    [JsonProperty("id")] public string Id;
    [JsonProperty("nodes")] public List<SkillTreeNode> Nodes = new();
    [JsonProperty("connectors")] public List<SkillTreeConnector> Connectors = new();
}
}