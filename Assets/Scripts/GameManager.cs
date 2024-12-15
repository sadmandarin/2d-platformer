using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Скрипт, управляющий основными состояниями игры
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;  

    private bool _levelLost;
    private bool _levelWin;
    [SerializeField] private bool _isDialogActive = false;
    
    private BossBase _boss;
    private PlayerInput _playerInputs;
    private Inputs _inputs;

    private Player _player;
    private PlayerMovement _movement;
    private PlayerAttack _playerAttack;
    private ActivateQuest _activateQuest;
    private TriggerToOpenDoor _triggerToOpenDoor;
    private DoorToSubLocation _doorToSubLocation;

    private DialogManager _dialogManager;

    public event Action OnLevelLost;
    public event Action OnLevelWin;
    public event Action OnGamePaused;

    public bool IsDialogActive { get { return _isDialogActive; } set { _isDialogActive = value; } }
    public bool IsGamePaused = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Удаляем лишние экземпляры
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        Debug.Log("Instance Инициализирован");

        Time.timeScale = 1.0f;

        _playerInputs = GetComponent<PlayerInput>();
        Debug.Log("playerInput Initialize");
        _inputs = new Inputs();
        Debug.Log("Input создан");
    }

    private void OnDisable()
    {
        UnsubscribeAllGamePlayAction();
    }

    private void OnDestroy()
    {
        _player.OnDied -= LevelLost;

        if (_boss != null)
            _boss.BossDead -= LevelWin;
    }

    public void InitializeComponent<T>(T component)
    {
        Debug.Log("Инициализация " + component);

        if (component == null) return;

        switch (component)
        {
            case Player player:
                _player = player;
                _player.Subscription();
                _player.OnDied += LevelLost;
                break;

            case PlayerMovement movement:
                _movement = movement;
                _movement.Subscription();
                break;

            case PlayerAttack playerAttack:
                _playerAttack = playerAttack;
                _playerAttack.Subscription();
                break;

            case TriggerToOpenDoor openDoor:
                _triggerToOpenDoor = openDoor;
                _triggerToOpenDoor.Subscription();
                break;

            case DoorToSubLocation door:
                _doorToSubLocation = door;
                _doorToSubLocation.Subscription();
                break;

            case DialogManager dialog:
                _dialogManager = dialog;
                break;

            case BossBase boss:
                _boss = boss;
                _boss.BossDead += LevelWin;
                break;

            default:
                Debug.LogWarning($"Unsupported component type: {typeof(T)}");
                break;
        }
    }

    public void EnterDialog()
    {
        if (_player != null)
        {
            Debug.Log("Enter");

            IsDialogActive = true;

            _playerInputs.SwitchCurrentActionMap("Dialogs");

            UnsubscribeAllGamePlayAction();
            SubscribeDialogAction();

            Debug.Log("Current Action Map: " + _playerInputs.currentActionMap.name);
        }
    }

    public void ExitDialogs()
    {
        if (_player != null)
        {
            Debug.Log("Exit");

            IsDialogActive = false;

            Debug.Log(IsDialogActive);

            _playerInputs.SwitchCurrentActionMap("GamePlay");

            SubscribeAllGamePlayAction();
            UnsubscribeDialogAction();
        }
    }

    public void LevelLost()
    {
        _levelLost = true;
        OnLevelLost?.Invoke();
    }

    public void LevelWin()
    {
        _levelWin = true;
        OnLevelWin?.Invoke();
    }

    public void GamePaused()
    {
        IsGamePaused = true;
        Time.timeScale = 0f;
        OnGamePaused?.Invoke();
    }

    //Доделать, когда будет готово управление в UI
    public void ExitGamePaused()
    {
        IsGamePaused = false;
        Time.timeScale = 1f;
        if (IsDialogActive)
        {
            _playerInputs.actions.FindActionMap("GamePlay").Disable();
            _playerInputs.SwitchCurrentActionMap("Dialogs");
        }
            
        else
        {
            _playerInputs.actions.FindActionMap("GamePlay").Disable();
            _playerInputs.SwitchCurrentActionMap("GamePlay");
        }
            
    }

    private void SubscribeAllGamePlayAction()
    {
        _player.Subscription();
        _movement.Subscription();
        _playerAttack.Subscription();
        _triggerToOpenDoor?.Subscription();
        _doorToSubLocation.Subscription();
    }

    private void UnsubscribeAllGamePlayAction()
    {
        _player.Unsubscription();
        _playerAttack.Unsubscription();
        _movement.Unsubscription();
        _triggerToOpenDoor.Unsubscription();
        _doorToSubLocation?.Unsubscription();
    }

    private void SubscribeDialogAction()
    {
        _dialogManager.Subscription();
    }

    private void UnsubscribeDialogAction()
    {
        _dialogManager.Unsubscription();
    }
}
