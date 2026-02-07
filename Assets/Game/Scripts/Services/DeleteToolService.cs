using System;
using UnityEngine;

namespace Services {
public class DeleteToolService : MonoBehaviour, ITool {
    public static DeleteToolService Instance;

    public event Action Changed;

    public bool IsEnabled { get; private set; }

    private void Awake() {
        Instance = this;
    }

    public void Toggle() {
        IsEnabled = !IsEnabled;
        Changed?.Invoke();
    }
}
}