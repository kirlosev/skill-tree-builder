using StbData;
using UnityEngine;

namespace Ui {
public class SkillTreeLinkView : MonoBehaviour {
    private SkillTreeLinkData _data;

    public void Setup(SkillTreeLinkData data, SkillTreeNodeView fromView, SkillTreeNodeView toView) {
        _data = data;
    }
}
}