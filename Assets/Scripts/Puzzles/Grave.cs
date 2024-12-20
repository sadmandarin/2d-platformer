using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class Grave : MonoBehaviour
{
    [SerializeField] private TextMeshPro _textName;

    private GravesPuzzle1Level _puzzle;
    private Inputs _inputs;

    public int Index;
    public bool IsPressed;

    private void Awake()
    {
        _inputs = new Inputs();
        _puzzle = GetComponentInParent<GravesPuzzle1Level>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() && !_puzzle.PuzzleComplete)
        {
            _textName.enabled = true;

            Subscription();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() && !_puzzle.PuzzleComplete)
        {
            _textName.enabled = false;

            Unsubscription();
        }
    }

    private void Subscription()
    {
        Debug.Log("��������");

        _inputs.GamePlay.Interaction.performed += OnGravePressed;

        _inputs.Enable();
    }

    private void Unsubscription()
    {
        Debug.Log("�������" + Index);

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
