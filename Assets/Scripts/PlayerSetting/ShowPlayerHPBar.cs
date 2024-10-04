using UnityEngine;
using UnityEngine.UI;

public class ShowPlayerHPBar : MonoBehaviour
{
    private Player _playerHp;
    private Image _image;

    private void OnEnable()
    {
        _playerHp = FindFirstObjectByType<Player>();

        _image = GetComponent<Image>();

        _playerHp.OnHPChanged += UpdateHPBar;
    }

    private void Start()
    {
        UpdateHPBar();
    }

    private void OnDisable()
    {
        _playerHp.OnHPChanged -= UpdateHPBar;
    }

    void UpdateHPBar()
    {
        _image.fillAmount = _playerHp.PlayerHp / _playerHp.MaxHP;
    }
}
