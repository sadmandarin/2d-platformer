using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Скрипт, управляющий основными состояниями игры
/// </summary>
public class GameManager : MonoBehaviour
{
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
        Time.timeScale = 1.0f;

        _movement = FindFirstObjectByType<PlayerMovement>();
        _player = FindFirstObjectByType<Player>();
        _playerAttack = FindFirstObjectByType<PlayerAttack>();
        //  _activateQuest = FindFirstObjectByType<ActivateQuest>();
        _triggerToOpenDoor = FindFirstObjectByType<TriggerToOpenDoor>();
        _doorToSubLocation = FindFirstObjectByType<DoorToSubLocation>();

        _dialogManager = FindFirstObjectByType<DialogManager>();

        _boss = FindFirstObjectByType<BossBase>();
        _playerInputs = GetComponent<PlayerInput>();
        _inputs = new Inputs();

        _player.OnDied += LevelLost;
        
        if(_boss != null)
            _boss.BossDead += LevelWin;
    }

    private void OnEnable()
    {
        SubscribeAllGamePlayAction();
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
        _player?.Subscription();
        _playerAttack?.Subscription();
        _movement?.Subscription();
        _activateQuest?.Subscription();
        _triggerToOpenDoor?.Subscription();
        _doorToSubLocation?.Subscription();
    }

    private void UnsubscribeAllGamePlayAction()
    {
        _player?.Unsubscription();
        _playerAttack?.Unsubscription();
        _movement?.Unsubscription();
        _activateQuest?.Unsubscription();
        _triggerToOpenDoor?.Unsubscription();
        _doorToSubLocation?.Unsubscription();
    }

    private void SubscribeDialogAction()
    {
        _dialogManager?.Subscription();
    }

    private void UnsubscribeDialogAction()
    {
        _dialogManager?.Unsubscription();
    }
}
