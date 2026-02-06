using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace StbData {

[Serializable]
public class SkillTreeNodeData {
    [JsonProperty("id")] public int Id;
    [JsonProperty("position")] public Vector2 Position;
    [JsonProperty("data")] public Dictionary<string, string> Data = new();
}

[Serializable]
public class SkillTreeConnectorData {
    [JsonProperty("from_id")] public int FromNodeId;
    [JsonProperty("to_id")] public int ToNodeId;
    [JsonProperty("is_two_way")] public bool IsTwoWay = true;
}

[Serializable]
public class SkillTreeData {
    [JsonProperty("version")] public int Version;
    [JsonProperty("id")] public string Id;
    [JsonProperty("anchor_position")] public Vector2 AnchorPosition;
    [JsonProperty("grid_size")] public int GridSize = 48;
    [JsonProperty("nodes")] public List<SkillTreeNodeData> Nodes = new();
    [JsonProperty("connectors")] public List<SkillTreeConnectorData> Connectors = new();
}
}