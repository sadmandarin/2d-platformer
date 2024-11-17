using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class Grave : MonoBehaviour
{
    public int Index;
    public bool IsPressed;

    private GravesPuzzle1Level _puzzle;
    private Inputs _inputs;

    private void Awake()
    {
        _inputs = new Inputs();
        _puzzle = GetComponentInParent<GravesPuzzle1Level>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            Subscription();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            Unsubscription();
        }
    }

    private void Subscription()
    {
        Debug.Log("Подписка");

        _inputs.GamePlay.Interaction.performed += OnGravePressed;

        _inputs.Enable();
    }

    private void Unsubscription()
    {
        Debug.Log("Отписка" + Index);

        _inputs.GamePlay.Interaction.performed -= OnGravePressed;

        _inputs.Enable();
    }

    private void OnGravePressed(InputAction.CallbackContext context)
    {
        if (!IsPressed)
        {
            IsPressed = true;
            _puzzle.PressGrave(Index);
        }
    }
}
