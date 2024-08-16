using UnityEngine;
using UnityEngine.UI;

public class ShowPlayerHp : MonoBehaviour
{
    [SerializeField] Player _playerHp;

    private Text _text;

    private void OnEnable()
    {
        _text = GetComponent<Text>();

        UpdateHPText();

        _playerHp.OnTookDamage += UpdateHPText;
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
