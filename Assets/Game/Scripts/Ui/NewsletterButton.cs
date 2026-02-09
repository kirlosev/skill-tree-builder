using UnityEngine;
using UnityEngine.UI;

namespace Ui {
public class NewsletterButton : MonoBehaviour {
    private Button _button;

    private void Awake() {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClicked);
    }

    private void OnClicked() {
        Application.OpenURL("https://kirlosev.beehiiv.com/");
    }
}
}