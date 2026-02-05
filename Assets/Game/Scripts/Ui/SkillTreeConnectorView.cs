using StbData;
using UnityEngine;

namespace Ui {
public class SkillTreeConnectorView : MonoBehaviour {
    private SkillTreeConnectorData _data;

    public void Setup(SkillTreeConnectorData data, SkillTreeNodeView fromView, SkillTreeNodeView toView) {
        _data = data;
    }
}
}