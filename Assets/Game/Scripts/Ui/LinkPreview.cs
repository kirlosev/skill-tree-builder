using Services;
using UnityEngine;

namespace Ui {
public class LinkingPreview : MonoBehaviour {
    [SerializeField] private RectTransform _line;

    private RectTransform _parent;
    private RectTransform _rect;
    private bool _isShown;
    private Vector2 _downLocalPos;

    private void Awake() {
        _parent = (RectTransform)transform.parent;
        _rect = (RectTransform)transform;
    }

    private void Start() {
        Hide();

        LinkToolService.Instance.StartedLinking += OnStartedLinking;
        LinkToolService.Instance.StoppedLinking += OnStoppedLinking;
    }

    private void OnDestroy() {
        LinkToolService.Instance.StartedLinking -= OnStartedLinking;
        LinkToolService.Instance.StoppedLinking -= OnStoppedLinking;
    }

    private Vector2 GetLocalPos() {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _parent,
            Input.mousePosition,
            Camera.main,
            out var localPos
        );
        return localPos;
    }

    private void OnStartedLinking() {
        var localPos = GetLocalPos();
        _downLocalPos = localPos;
        _rect.anchoredPosition = localPos;
        Show();
    }

    private void OnStoppedLinking() {
        Hide();
    }

    private void Show() {
        _isShown = true;
        _line.gameObject.SetActive(true);
    }

    private void Hide() {
        _isShown = false;
        _line.gameObject.SetActive(false);
    }

    private void Update() {
        if (!_isShown) {
            return;
        }

        var localPos = GetLocalPos();
        var startOffset = localPos - _downLocalPos;
        _line.sizeDelta = new Vector2(startOffset.magnitude, _line.sizeDelta.y);
        _rect.right = startOffset.normalized;
    }
}
}