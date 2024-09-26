using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ����������� �� ������
/// </summary>
public class ShowPlayerHp : MonoBehaviour
{
    private Player _playerHp;
    private Text _text;

    private void OnEnable()
    {
        _playerHp = FindFirstObjectByType<Player>();

        _text = GetComponent<Text>();

        _playerHp.OnTookDamage += UpdateHPText;
    }

    private void Start()
    {
        UpdateHPText();
    }

    private void OnDisable()
    {
        _playerHp.OnTookDamage -= UpdateHPText;
    }

    void UpdateHPText()
    {
        int currHp = (int)_playerHp.PlayerHp;
        _text.text = currHp.ToString();
    }
}
