using System.Collections.Generic;
using Services;
using StbData;
using UnityEngine;
using UnityEngine.Serialization;

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

        foreach (var c in _data.Links) {
            var fromView = _nodeViews[c.FromNodeId];
            var toView = _nodeViews[c.ToNodeId];
            var v = Instantiate(_linkPrefab, fromView.Pos, Quaternion.identity, _linkHolder);
            v.Setup(c, fromView, toView);

            // TODO save links
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