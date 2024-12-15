using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Открытие двери
/// </summary>
public class TriggerToOpenDoor : MonoBehaviour
{
    public GameObject Door;
    private bool _canOpenTheDoor;
    private bool _isDoorOpen;
    private Inputs _inputs;

    private void OnEnable()
    {
        _inputs = new Inputs();

        _isDoorOpen = false;

        GameManager.Instance.InitializeComponent(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_isDoorOpen && collision.gameObject.GetComponent<Player>())
        {
            Debug.Log("Press E to open the door");
            _canOpenTheDoor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!_isDoorOpen && collision.gameObject.GetComponent<Player>())
        {
            _canOpenTheDoor = false;
        }
    }

    public void Subscription()
    {
        _inputs.GamePlay.Interaction.performed += OpenTheDoor;

        _inputs.Enable();
    }

    public void Unsubscription()
    {
        _inputs.GamePlay.Interaction.performed -= OpenTheDoor;

        _inputs.Disable();
    }

    void OpenTheDoor(InputAction.CallbackContext cotext)
    {
        if (_canOpenTheDoor)
        {
            Door.SetActive(false);
            _canOpenTheDoor = false;
            _isDoorOpen = true;
        }
    }
}
