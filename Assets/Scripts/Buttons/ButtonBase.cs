using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public abstract class ButtonBase : MonoBehaviour
{
    [SerializeField] public ButtonBase _leftButton;
    [SerializeField] public ButtonBase _rightButton;
    [SerializeField] public ButtonBase _upButton;
    [SerializeField] public ButtonBase _downButton;
    [SerializeField] protected bool _hasSubMenu;
    [SerializeField] protected bool _isSelected;
    [SerializeField] protected bool _isNavigating;

    protected virtual void Update()
    {
        if (_isSelected)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                OnClick();
            }
        }
    }

    public void SelectButton()
    {
        GetComponent<Image>().color = Color.blue;

        Debug.Log(gameObject.name);

        _isSelected = true;
    }

    public void Deselect()
    {
        GetComponent<Image>().color = Color.white;

        Debug.Log(gameObject.name);

        _isSelected = false;
    }

    protected void OnClick()
    {
        if (_hasSubMenu)
            LoadSubMenu();
        else
            DoAction();
    }

    protected void Navigate()
    {
        if (!_isNavigating && Input.GetKeyDown(KeyCode.LeftArrow) && _leftButton != null)
        {
            NavigateToButton(_leftButton);
            _isNavigating = true;
        }
        else if (!_isNavigating && Input.GetKeyDown(KeyCode.RightArrow) && _rightButton != null)
        {
            NavigateToButton(_rightButton);
            _isNavigating = true;
        }
        else if (!_isNavigating && Input.GetKeyDown(KeyCode.UpArrow) && _upButton != null)
        {
            NavigateToButton(_upButton);
            _isNavigating = true;
        }
        else if (!_isNavigating && Input.GetKeyDown(KeyCode.DownArrow) && _downButton != null)
        {
            NavigateToButton(_downButton);
            _isNavigating = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) ||
            Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            _isNavigating = false;
        }
    }

    private void NavigateToButton(ButtonBase newButton)
    {
        Deselect();

        newButton.SelectButton();
    }

    protected abstract void DoAction();
    protected abstract void LoadSubMenu();
}
