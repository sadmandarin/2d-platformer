using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [SerializeField] private DialogsSO _mainDialog;
    [SerializeField] private DialogsSO _currentDialog;
    [SerializeField] private GameObject _dialogWindow;
    [SerializeField] private Text _speakerName;
    [SerializeField] private Text _line;
    [SerializeField] private List<GameObject> _choices;

    private float _letterDelay = 0.05f;
    private int _currentLineIndex = 0;
    private int _mainDialogLastLineIndex = 0;
    private bool _isTyping = false;
    private bool _isSubDialogActiveNow = false;

    private List<bool> _activeChoices = new() { false, false, false };

    private Coroutine _typingCoroutine;
    private Inputs _inputs;
    private GameManager _gameManager;

    public bool IsDialogActive;

    private void Awake()
    {
        _gameManager = FindFirstObjectByType<GameManager>();
    }

    public void StartDialog()
    {
        _gameManager.EnterDialog();

        _inputs = new Inputs();
        _dialogWindow.SetActive(true);
        IsDialogActive = true;
        _currentDialog = _mainDialog;
        _currentLineIndex = 0;

        _inputs.Dialogs.Interaction.performed += ManageDialog;
        _inputs.Dialogs.Option1.performed += FirstChoice;
        _inputs.Dialogs.Option2.performed += SecondChoice;
        _inputs.Dialogs.Option3.performed += ThirdChoice;

        _inputs.Enable();

        ShowNextLine();
    }

    private void OnDestroy()
    {
        _inputs.GamePlay.Interaction.performed -= ManageDialog;
        _inputs.Dialogs.Option1.performed -= FirstChoice;
        _inputs.Dialogs.Option2.performed -= SecondChoice;
        _inputs.Dialogs.Option3.performed -= ThirdChoice;

        _inputs.Disable();
    }

    // Переход к следующей реплике
    private void ShowNextLine()
    {
        if (_currentLineIndex >= _currentDialog.Dialogs.Count)
        {
            EndDialog();
            return;
        }

        Speech currentSpeech = _currentDialog.Dialogs[_currentLineIndex];

        _speakerName.text = currentSpeech.SpeakerName; // Устанавливаем имя говорящего
        _line.text = ""; // Очищаем текст для новой реплики

        if (_typingCoroutine != null)
        {
            StopCoroutine(_typingCoroutine);
        }

        _typingCoroutine = StartCoroutine(TypeSentence(currentSpeech.Line));
    }

    // Вывод реплики побуквенно
    private IEnumerator TypeSentence(string sentence)
    {
        _isTyping = true;

        foreach (char letter in sentence.ToCharArray())
        {
            _line.text += letter;
            yield return new WaitForSeconds(_letterDelay);
        }

        _isTyping = false;

        Speech curentSpeech = _currentDialog.Dialogs[_currentLineIndex];

        if (curentSpeech.HasChoices)
            ShowDialogOptions();

    }

    private void ManageDialog(InputAction.CallbackContext context)
    {
        if (_isTyping)
        {
            // Если реплика еще не полностью напечатана, выводим сразу весь текст
            StopCoroutine(_typingCoroutine);
            _line.text = _currentDialog.Dialogs[_currentLineIndex].Line;
            _isTyping = false;
        }
        else
        {
            Speech curentSpeech = _currentDialog.Dialogs[_currentLineIndex];

            if (curentSpeech.HasChoices)
                ShowDialogOptions();
            else
            {
                _currentLineIndex++;
                ShowNextLine();
            }
        }
    }

    private void ShowDialogOptions()
    {
        Speech currentSpeech = _currentDialog.Dialogs[_currentLineIndex];

        for (int i = 0; i < currentSpeech.Choices.Count; i++)
        {
            _choices[i].SetActive(true);
            _activeChoices[i] = true;
        }
    }

    private void HideDialogOption()
    {
        Speech currentSpeech = _currentDialog.Dialogs[_currentLineIndex];

        for (int i = 0; i < currentSpeech.Choices.Count; i++)
        {
            _choices[i].SetActive(false);
            _activeChoices[i] = false;
        }
    }

    private void FirstChoice(InputAction.CallbackContext context)
    {
        if (_activeChoices[0] == true && !_isSubDialogActiveNow)
        {
            EnterSubDialog(0);   
        }
        else
        {
            ExitSubDialog();
        }
    }

    private void SecondChoice(InputAction.CallbackContext context)
    {
        if (_activeChoices[1] == true && !_isSubDialogActiveNow)
        {
            EnterSubDialog(1);
        }
        else
        {
            ExitSubDialog();
        }
    }

    private void ThirdChoice(InputAction.CallbackContext context)
    {
        if (_activeChoices[2] == true && !_isSubDialogActiveNow)
        {
            EnterSubDialog(2);
        }
        else
        {
            ExitSubDialog();
        }
    }

    private void EnterSubDialog(int number)
    {
        HideDialogOption();

        _isSubDialogActiveNow = true;

        _mainDialogLastLineIndex = _currentLineIndex;

        _currentDialog = _currentDialog.Dialogs[_currentLineIndex].NextDialogs[number];
        _currentLineIndex = 0;

        ShowNextLine();
    }

    private void ExitSubDialog()
    {
        HideDialogOption();

        _currentLineIndex = _mainDialogLastLineIndex;

        _currentDialog = _mainDialog;

        ShowNextLine();

        _isSubDialogActiveNow = false;
    }

    // Завершение диалога
    private void EndDialog()
    {
        if (!_isSubDialogActiveNow)
        {
            _line.text = "";
            _speakerName.text = "";
            Debug.Log("Диалог завершен");
            _gameManager.ExitDialogs();
            _dialogWindow.SetActive(false);
        }
        else
        {
            ExitSubDialog();
        }
    }
}
