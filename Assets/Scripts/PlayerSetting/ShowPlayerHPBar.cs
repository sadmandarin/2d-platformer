using UnityEngine;
using UnityEngine.UI;

public class ShowPlayerHPBar : MonoBehaviour
{
    [SerializeField] private Player _playerHp;
    private Image _image;

    private void OnEnable()
    {
        _image = GetComponent<Image>();

        _playerHp.OnTookDamage += UpdateHPBar;
    }

    private void OnDisable()
    {
        _playerHp.OnTookDamage -= UpdateHPBar;
    }

    void UpdateHPBar()
    {
        _image.fillAmount = _playerHp.PlayerHp / _playerHp.MaxHP;
    }
}
