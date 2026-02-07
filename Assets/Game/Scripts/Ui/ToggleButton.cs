using UnityEngine;
using UnityEngine.UI;

namespace Ui {
public abstract class ToggleButton : MonoBehaviour {
    [SerializeField] private Image _background;
    [SerializeField] private Sprite _backgroundOnSprite;
    [SerializeField] private Sprite _backgroundOffSprite;
    [SerializeField] private Color _backgroundOnColor;

    private Button _button;
    protected abstract bool IsOn { get; }
    protected abstract KeyCode Hotkey { get; }

    private void Awake() {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClicked);
    }

    protected abstract void OnClicked();

    protected virtual void Start() {
        Refresh();
    }

    protected virtual void OnDestroy() {
    }

    private void Update() {
        if (Input.GetKeyDown(Hotkey)) {
            OnClicked();
            Refresh();
        }
    }

    protected void Refresh() {
        _background.sprite = IsOn ? _backgroundOnSprite : _backgroundOffSprite;
        _background.color = IsOn ? _backgroundOnColor : Color.white;
    }
}
}