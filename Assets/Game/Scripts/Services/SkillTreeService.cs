using System;
using System.Linq;
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
    public event Action<SkillTreeNodeData> RemovedNode;
    public event Action<int, int> RemovedLink;
    public event Action<string> Exported;
    public event Action<SkillTreeNodeData> NodeDataDefaultsUpdated;
    public event Action<SkillTreeNodeData> NodeDataChanged;

    private SkillTreeData _data;
    private int _lastNodeId;

    public bool HasTree => _data != null;

    private void Awake() {
        Instance = this;
    }

    public void CreateNewTree() {
        _lastNodeId = 0;
        var firstNodeData = new SkillTreeNodeData() {
            Id = _lastNodeId
        };
        _data = new SkillTreeData() {
            Id = "new_tree",
            Nodes = new List<SkillTreeNodeData>() { firstNodeData },
            AnchorPosition = Vector2.zero,
            Links = new List<SkillTreeLinkData>()
        };
        TreeCreated?.Invoke(_data);
    }

    public void LoadTree(string json) {
        var jsonContent = json;
        var settings = new JsonSerializerSettings
        {
            Converters = new List<JsonConverter> { new Vector2Converter() }
        };

        _data = JsonConvert.DeserializeObject<SkillTreeData>(jsonContent, settings);
        _lastNodeId = _data.Nodes.Max(x => x.Id);
        TreeCreated?.Invoke(_data);
    }

    public void AddNode() {
        if (_data == null) {
            return;
        }

        _lastNodeId++;
        var lastNode = _data.Nodes[_data.Nodes.Count - 1];
        var pos = lastNode.Position + Vector2.right * (_data.GridSize * 2f);
        var nodeData = new SkillTreeNodeData() {
            Id = _lastNodeId, Position = pos
        };
        foreach (var (dKey, dValue) in _data.NodeDataDefaults.Data) {
            nodeData.Data[dKey] = dValue;
        }

        _data.Nodes.Add(nodeData);
        NodeAdded?.Invoke(nodeData);
    }

    public void LinkNodes(SkillTreeNodeView fromNode, SkillTreeNodeView toNode) {
        var alreadyLinked = _data.Links.Count(x => x.FromNodeId == fromNode.Data.Id && x.ToNodeId == toNode.Data.Id) > 0;
        if (alreadyLinked) {
            return;
        }

        var link = new SkillTreeLinkData() {
            FromNodeId = fromNode.Data.Id,
            ToNodeId = toNode.Data.Id
        };
        _data.Links.Add(link);
        LinkAdded?.Invoke(link);
        // if IsTwoWay
        var reverse = new SkillTreeLinkData() {
            FromNodeId = toNode.Data.Id,
            ToNodeId = fromNode.Data.Id
        };
        _data.Links.Add(reverse);
        LinkAdded?.Invoke(reverse);
    }

    public List<int> GetNodeLinks(int id) {
        return _data.Links.Where(x => x.FromNodeId == id).Select(x=>x.ToNodeId).ToList();
    }

    public void RemoveLink(int fromNodeId, int toNodeId) {
        _data.Links.RemoveAll(x =>
            x.FromNodeId == fromNodeId && x.ToNodeId == toNodeId ||
            x.ToNodeId == fromNodeId && x.FromNodeId == toNodeId
        );
        RemovedLink?.Invoke(fromNodeId, toNodeId);
    }

    public void DeleteNode(SkillTreeNodeView nodeView) {
        var data = nodeView.Data;
        var nodeId = data.Id;

        var rmLinks = new List<SkillTreeLinkData>();
        foreach (var l in _data.Links) {
            if (l.FromNodeId == nodeId || l.ToNodeId == nodeId) {
                rmLinks.Add(l);
            }
        }

        foreach (var rml in rmLinks) {
            var fromNodeId = rml.FromNodeId;
            var toNodeId = rml.ToNodeId;
            _data.Links.Remove(rml);
            RemovedLink?.Invoke(fromNodeId, toNodeId);
        }

        _data.Nodes.RemoveAll(x => x.Id == nodeId);
        RemovedNode?.Invoke(data);
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

    public SkillTreeNodeData GetDataDefaults() {
        return _data.NodeDataDefaults;
    }

    public float GetGridSize() {
        return _data.GridSize;
    }

    public void SetNodeDataDefaults(SkillTreeNodeData data) {
        _data.NodeDataDefaults = data;
        NodeDataDefaultsUpdated?.Invoke(_data.NodeDataDefaults);
    }

    public void RaiseNodeDataChanged(SkillTreeNodeData data) {
        NodeDataChanged?.Invoke(data);
    }
}
}