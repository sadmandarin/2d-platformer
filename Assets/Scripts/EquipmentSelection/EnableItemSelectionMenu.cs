using UnityEngine;

/// <summary>
/// ������ ��� ��������� ������ ���������� �� ��������� �������
/// </summary>
/// <remarks>
/// ���������� ��� ������ � ������
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
