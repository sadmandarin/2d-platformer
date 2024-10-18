using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// —крипт, управл€ющий основными состо€ни€ми игры
/// </summary>
public class GameManager : MonoBehaviour
{
    private bool _levelLost;
    private bool _levelWin;
    private Player _player;
    private BossBase _boss;
    private PlayerInput _playerInputs;

    public event Action OnLevelLost;
    public event Action OnLevelWin;
    public event Action OnGamePaused;

    public bool IsDialogActive = false;
    public bool IsGamePaused = false;

    private void Awake()
    {
        Time.timeScale = 1.0f;

        _player = FindFirstObjectByType<Player>();
        _boss = FindFirstObjectByType<BossBase>();
        _playerInputs = GetComponent<PlayerInput>();

        _player.OnDied += LevelLost;
        _boss.BossDead += LevelWin;
    }

    private void OnDestroy()
    {
        _player.OnDied -= LevelLost;
        _boss.BossDead -= LevelWin;
    }

    public void EnterDialog()
    {
        if (_player != null)
        {
            Debug.Log("Enter");

            IsDialogActive = true;

            _playerInputs.actions.FindActionMap("GamePlay").Disable();
            _playerInputs.SwitchCurrentActionMap("Dialogs");

            Debug.Log("Current Action Map: " + _playerInputs.currentActionMap.name);
        }
    }

    public void ExitDialogs()
    {
        if (_player != null)
        {
            Debug.Log("Exit");

            IsDialogActive = false;
            _playerInputs.SwitchCurrentActionMap("GamePlay");
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
}
