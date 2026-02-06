using System.Collections.Generic;
using Services;
using StbData;
using UnityEngine;

namespace Ui {
public class SkillTreeView : MonoBehaviour {
    [SerializeField] private SkillTreeNodeView _nodePrefab;
    [SerializeField] private SkillTreeConnectorView _connectorPrefab;

    [SerializeField] private RectTransform _nodeHolder;
    [SerializeField] private RectTransform _connectorHolder;

    private RectTransform _rect;
    private SkillTreeData _data;
    private Dictionary<int, SkillTreeNodeView> _nodeViews = new();
    private List<SkillTreeConnectorView> _connectorViews = new();

    private void Awake() {
        _rect = (RectTransform)transform;
    }

    private void Start() {
        SkillTreeService.Instance.TreeCreated += OnNewTreeCreated;
        SkillTreeService.Instance.NodeAdded += OnNodeAdded;
    }

    private void OnDestroy() {
        SkillTreeService.Instance.TreeCreated -= OnNewTreeCreated;
        SkillTreeService.Instance.NodeAdded -= OnNodeAdded;
    }

    private void OnNewTreeCreated(SkillTreeData newTreeData) {
        RemoveCurrentTree();
        Setup(newTreeData);
    }

    private void OnNodeAdded(SkillTreeNodeData newNodeData) {
        SpawnNode(newNodeData);
    }

    private void RemoveCurrentTree() {
        foreach (Transform n in _nodeHolder) {
            Destroy(n.gameObject);
        }
        _nodeViews.Clear();

        foreach (Transform c in _connectorHolder) {
            Destroy(c.gameObject);
        }
        _connectorViews.Clear();
    }

    public void Setup(SkillTreeData data) {
        _data = data;

        foreach (var n in _data.Nodes) {
            SpawnNode(n);
        }

        foreach (var c in _data.Connectors) {
            var fromView = _nodeViews[c.FromNodeId];
            var toView = _nodeViews[c.ToNodeId];
            var v = Instantiate(_connectorPrefab, fromView.Pos, Quaternion.identity, _connectorHolder);
            v.Setup(c, fromView, toView);

            // TODO save connectors
        }
    }

    private void SpawnNode(SkillTreeNodeData nodeData) {
        var v = Instantiate(_nodePrefab, _nodeHolder);
        ((RectTransform)v.transform).anchoredPosition = nodeData.Position;
        v.Setup(nodeData);
        _nodeViews[nodeData.Id] = v;
    }
}
}