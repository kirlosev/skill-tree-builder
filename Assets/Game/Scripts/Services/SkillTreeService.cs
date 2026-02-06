using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using StbData;
using Ui;
using UnityEngine;

namespace Services {
public class SkillTreeService : MonoBehaviour {
    public static SkillTreeService Instance;

    public event Action<SkillTreeData> TreeCreated;
    public event Action<SkillTreeNodeData> NodeAdded;
    public event Action<SkillTreeLinkData> LinkAdded;
    public event Action<string> Exported;

    private SkillTreeData _data;
    private int _lastNodeId;

    private void Awake() {
        Instance = this;
    }

    public void CreateNewTree() {
        _lastNodeId = 0;
        var firstNodeData = new SkillTreeNodeData() {
            Id = _lastNodeId, Position = Vector2.zero, Data = new()
        };
        _data = new SkillTreeData() {
            Id = "new_tree",
            Nodes = new List<SkillTreeNodeData>() { firstNodeData },
            AnchorPosition = Vector2.zero,
            Links = new List<SkillTreeLinkData>()
        };
        TreeCreated?.Invoke(_data);
    }

    public void LoadTree() {
        throw new NotImplementedException();
        // TODO set max node id
        //  _lastNodeId =
    }

    public void AddNode() {
        if (_data == null) {
            return;
        }

        _lastNodeId++;
        var lastNode = _data.Nodes[_data.Nodes.Count - 1];
        var pos = lastNode.Position + Vector2.right * _data.GridSize * 2f;
        var nodeData = new SkillTreeNodeData() {
            Id = _lastNodeId, Position = pos, Data = new()
        };
        _data.Nodes.Add(nodeData);
        NodeAdded?.Invoke(nodeData);
    }

    public void LinkNodes(SkillTreeNodeView fromNode, SkillTreeNodeView toNode) {
        var link = new SkillTreeLinkData() {
            FromNodeId = fromNode.Data.Id,
            ToNodeId = toNode.Data.Id,
            IsTwoWay = true
        };
        _data.Links.Add(link);
        LinkAdded?.Invoke(link);
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

    public void RemoveLink(SkillTreeNodeView fromNode, SkillTreeNodeView toNode) {
        throw new NotImplementedException();
    }
}
}