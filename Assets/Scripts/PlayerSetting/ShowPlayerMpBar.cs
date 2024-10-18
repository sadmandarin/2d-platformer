using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowPlayerMpBar : MonoBehaviour
{
    private Player _playerMp;
    private Image _image;

    private void OnEnable()
    {
        _playerMp = FindFirstObjectByType<Player>();

        _image = GetComponent<Image>();

        _playerMp.OnHPChanged += UpdateHPBar;
    }

    private void Start()
    {
        UpdateHPBar();
    }

    private void OnDisable()
    {
        _playerMp.OnMPChanged -= UpdateHPBar;
    }

    void UpdateHPBar()
    {
        _image.fillAmount = _playerMp.PlayerMp / _playerMp.MaxMP;
    }
}
