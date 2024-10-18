using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DoorToSubLocation : MonoBehaviour
{
    [SerializeField] private Transform _teleportLocatioon;
    [SerializeField] private Image _image;

    private Inputs _inputs;
    private Player _player;
    private bool _isPlayerNear = false;

    private void Awake()
    {
        _inputs = new Inputs();
    }

    private void OnEnable()
    {
        _inputs.GamePlay.Interaction.performed += StartTeleport;

        _inputs.Enable();
    }

    private void OnDisable()
    {
        _inputs.GamePlay.Interaction.performed -= StartTeleport;

        _inputs.Disable();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            _isPlayerNear = true;
            _player = collision.gameObject.GetComponent<Player>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            _isPlayerNear = false;
            _player = null;
        }
    }

    private void StartTeleport(InputAction.CallbackContext context)
    {
        if (_isPlayerNear && _player != null)
        {
            StartCoroutine(TeleportingToLocation());
        }
    }

    private IEnumerator TeleportingToLocation()
    {
        // Затемнение экрана
        float fadeDuration = 1f;
        Color fadeColor = _image.color;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            fadeColor.a = Mathf.Lerp(0, 1, t / fadeDuration); // Увеличиваем прозрачность до 1
            _image.color = fadeColor;
            yield return null;
        }

        fadeColor.a = 1;
        _image.color = fadeColor;

        _player.transform.position = _teleportLocatioon.position;

        // Обратное затемнение
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            fadeColor.a = Mathf.Lerp(1, 0, t / fadeDuration); // Уменьшаем прозрачность до 0
            _image.color = fadeColor;
            yield return null;
        }

        fadeColor.a = 0;
        _image.color = fadeColor;
    }
}
