using UnityEngine;

/// <summary>
/// Скрипт для включения выбора снаряжения на следующий уровень
/// </summary>
/// <remarks>
/// Вызывается при победе в уровне
/// </remarks>
public class EnableItemSelectionMenu : MonoBehaviour
{
    [SerializeField] private GameObject _selectionMenu;

    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = FindFirstObjectByType<GameManager>();
        _gameManager.OnLevelWin += EnableSelectionMenu;
    }

    private void OnDestroy()
    {
        _gameManager.OnLevelWin -= EnableSelectionMenu;
    }

    private void EnableSelectionMenu()
    {
        _selectionMenu.SetActive(true);
    }
}
