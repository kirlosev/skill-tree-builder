using System;
using System.Collections.Generic;
using Services;
using StbData;
using UnityEngine;

namespace Ui {
public class SkillTreeView : MonoBehaviour {
    [SerializeField] private SkillTreeNodeView _nodePrefab;
    [SerializeField] private SkillTreeLinkView _linkPrefab;

    [SerializeField] private RectTransform _nodeHolder;
    [SerializeField] private RectTransform _linkHolder;

    private RectTransform _rect;
    private SkillTreeData _data;
    private Dictionary<int, SkillTreeNodeView> _nodeViews = new();
    private List<SkillTreeLinkView> _linkViews = new();

    private void Awake() {
        _rect = (RectTransform)transform;
    }

    private void Start() {
        SkillTreeService.Instance.TreeCreated += OnNewTreeCreated;
        SkillTreeService.Instance.NodeAdded += OnNodeAdded;
        SkillTreeService.Instance.LinkAdded += OnLinkAdded;
        SkillTreeService.Instance.RemovedLink += OnLinkRemoved;
        SkillTreeService.Instance.RemovedNode += OnNodeRemoved;
        SkillTreeService.Instance.NodeDataChanged += OnNodeDataChanged;
    }

    private void OnDestroy() {
        SkillTreeService.Instance.TreeCreated -= OnNewTreeCreated;
        SkillTreeService.Instance.NodeAdded -= OnNodeAdded;
        SkillTreeService.Instance.LinkAdded -= OnLinkAdded;
        SkillTreeService.Instance.RemovedLink -= OnLinkRemoved;
        SkillTreeService.Instance.RemovedNode -= OnNodeRemoved;
        SkillTreeService.Instance.NodeDataChanged -= OnNodeDataChanged;
    }

    private void OnNewTreeCreated(SkillTreeData newTreeData) {
        RemoveCurrentTree();
        Setup(newTreeData);
    }

    private void OnNodeAdded(SkillTreeNodeData newNodeData) {
        SpawnNode(newNodeData);
    }

    private void OnLinkAdded(SkillTreeLinkData newLinkData) {
        SpawnLink(newLinkData);
    }

    private void OnLinkRemoved(int fromNodeId, int toNodeId) {
        for (var i = _linkViews.Count - 1; i >= 0; --i) {
            var v = _linkViews[i];
            if (v.Data.FromNodeId == fromNodeId && v.Data.ToNodeId == toNodeId ||
                v.Data.FromNodeId == toNodeId && v.Data.ToNodeId == fromNodeId) {
                Destroy(v.gameObject);
                _linkViews.RemoveAt(i);
            }
        }
    }

    private void OnNodeRemoved(SkillTreeNodeData nodeData) {
        var nodeId = nodeData.Id;
        var view = _nodeViews[nodeId];
        _nodeViews.Remove(nodeId);
        Destroy(view.gameObject);
    }

    private void OnNodeDataChanged(SkillTreeNodeData nodeData) {
        _nodeViews[nodeData.Id].Refresh();
    }

    private void RemoveCurrentTree() {
        foreach (Transform n in _nodeHolder) {
            Destroy(n.gameObject);
        }
        _nodeViews.Clear();

        foreach (Transform c in _linkHolder) {
            Destroy(c.gameObject);
        }
        _linkViews.Clear();
    }

    public void Setup(SkillTreeData data) {
        _data = data;

        foreach (var n in _data.Nodes) {
            SpawnNode(n);
        }

        foreach (var l in _data.Links) {
            SpawnLink(l);
        }
    }

    private void SpawnNode(SkillTreeNodeData nodeData) {
        var v = Instantiate(_nodePrefab, _nodeHolder);
        ((RectTransform)v.transform).anchoredPosition = nodeData.Position;
        v.Setup(nodeData);
        _nodeViews[nodeData.Id] = v;
    }

    private void SpawnLink(SkillTreeLinkData linkData) {
        var fromView = _nodeViews[linkData.FromNodeId];
        var toView = _nodeViews[linkData.ToNodeId];
        var v = Instantiate(_linkPrefab, _linkHolder);
        v.Setup(linkData, fromView, toView);
        _linkViews.Add(v);
    }

    public SkillTreeNodeView GetNodeOnPosition(Vector2 position) {
        foreach (var (id, view) in _nodeViews) {
            if (view.ContainsPosition(position)) {
                return view;
            }
        }
        return null;
    }
}
}