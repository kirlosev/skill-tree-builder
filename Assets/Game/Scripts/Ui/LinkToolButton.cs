using Services;
using UnityEngine;
using UnityEngine.UI;

namespace Ui {
public class LinkToolButton : MonoBehaviour {
    [SerializeField] private Image _background;
    [SerializeField] private Sprite _backgroundOnSprite;
    [SerializeField] private Sprite _backgroundOffSprite;
    [SerializeField] private Color _backgroundOnColor;

    private Button _button;

    private void Awake() {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClicked);
    }

    private void OnClicked() {
        LinkToolService.Instance.ToggleLinkTool();
    }

    private void Start() {
        LinkToolService.Instance.Changed += OnToolChanged;
        Refresh();
    }

    private void OnDestroy() {
        LinkToolService.Instance.Changed -= OnToolChanged;
    }

    private void OnToolChanged() {
        Refresh();
    }

    private void Refresh() {
        var isOn = LinkToolService.Instance.IsEnabled;
        _background.sprite = isOn ? _backgroundOnSprite : _backgroundOffSprite;
        _background.color = isOn ? _backgroundOnColor : Color.white;
    }
}
}