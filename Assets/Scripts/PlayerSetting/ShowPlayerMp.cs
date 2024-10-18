using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowPlayerMp : MonoBehaviour
{
    private Player _playerMp;
    private Text _text;

    private void OnEnable()
    {
        _playerMp = FindFirstObjectByType<Player>();

        _text = GetComponent<Text>();

        _playerMp.OnMPChanged += UpdateHPText;
    }

    private void Start()
    {
        UpdateHPText();
    }

    private void OnDisable()
    {
        _playerMp.OnHPChanged -= UpdateHPText;
    }

    void UpdateHPText()
    {
        int currHp = (int)_playerMp.PlayerMp;
        _text.text = currHp.ToString();
    }
}
