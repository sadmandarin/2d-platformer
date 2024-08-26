using UnityEngine;
using UnityEngine.UI;

public abstract class ButtonBase : MonoBehaviour
{
    protected Button _button;

    protected virtual void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
    }

    protected abstract void OnClick();
}
