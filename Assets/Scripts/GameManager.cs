using System;
using UnityEngine;

/// <summary>
/// —крипт, управл€ющий основными состо€ни€ми игры
/// </summary>
public class GameManager : MonoBehaviour
{
    private bool _levelLost;
    private bool _levelWin;
    private bool _gamepaused;
    private Player _player;
    private BossBase _boss;

    public event Action OnLevelLost;
    public event Action OnLevelWin;
    public event Action OnGamePaused;

    private void Awake()
    {
        Time.timeScale = 1.0f;
        _player = FindFirstObjectByType<Player>();
        _boss = FindFirstObjectByType<BossBase>();
        _player.OnDied += LevelLost;
        _boss.BossDead += LevelWin;
    }

    private void OnDestroy()
    {
        _player.OnDied -= LevelLost;
        _boss.BossDead -= LevelWin;
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
        _gamepaused = true;
        Time.timeScale = 0f;
        OnGamePaused?.Invoke();
    }
}
