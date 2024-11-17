using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Взятие квеста у NPC
/// </summary>
public class ActivateQuest : MonoBehaviour
{
    private bool _isAvaiableToTakeQuest = false;
    private QuestManager _questManager;
    private NPC _avaiableQuest;
    private Inputs _inputs;
    private DialogManager _dialogManager;
    private GameManager _gameManager;

    private void Awake()
    {
        _inputs = new Inputs();
        _questManager = GetComponent<QuestManager>();
        _gameManager = FindFirstObjectByType<GameManager>();
    }

    public void Subscription()
    {
        Debug.Log("Podписка");

        _inputs.GamePlay.Interaction.performed += OnTakeQuest;

        _inputs.Enable();
    }

    public void Unsubscription()
    {
        Debug.Log("Отписка");

        _inputs.GamePlay.Interaction.performed -= OnTakeQuest;

        _inputs.Disable();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<NPC>())
        {
            _avaiableQuest = collision.gameObject.GetComponent<NPC>();
            _dialogManager = collision.gameObject.GetComponent<DialogManager>();
            Subscription();

            _isAvaiableToTakeQuest = true;

            Debug.Log("Can Take quest");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<NPC>())
        {
            _avaiableQuest = null;
            _dialogManager = null;
            Unsubscription();

            _isAvaiableToTakeQuest = false;
        }
    }

    private void OnTakeQuest(InputAction.CallbackContext context)
    {
        if (_isAvaiableToTakeQuest)
            if (_avaiableQuest != null)
                if (!_avaiableQuest.IsQuestComplete)
                    if (_avaiableQuest.IsQuestActive == false && _dialogManager != null)
                    {
                        if (!_gameManager.IsDialogActive)
                        {
                            Debug.Log("Taked");
                            _dialogManager.StartDialog();
                        }
                    }

                    else
                    {
                        Debug.Log("Gived");
                        _questManager.CompleteQuest(_avaiableQuest.Quests[0]);
                        _avaiableQuest.CompleteQuest();
                    }
    }
}
