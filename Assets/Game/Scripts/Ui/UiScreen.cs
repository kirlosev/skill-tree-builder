using UnityEngine;

namespace Game.Scripts.Ui {
public abstract class UiScreen : MonoBehaviour {
    [SerializeField] protected Transform Content;

    public bool IsOpen { get; private set; }

    protected virtual void Start() {
        TurnOnOffByDefault();
    }

    protected abstract void TurnOnOffByDefault();

    public void TurnOn() {
        Content.gameObject.SetActive(true);
        IsOpen = true;
        TurnOnInternal();
    }

    protected virtual void TurnOnInternal() { }

    public void TurnOff() {
        Content.gameObject.SetActive(false);
        IsOpen = false;
        TurnOffInternal();
    }

    protected virtual void TurnOffInternal() { }
}
}