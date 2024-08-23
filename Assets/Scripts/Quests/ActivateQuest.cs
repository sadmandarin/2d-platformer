using UnityEngine;

public class ActivateQuest : MonoBehaviour
{
    private bool _isAvaiableToTakeQuest = false;
    private QuestManager _questManager;
    private NPC _avaiableQuest;

    private void Start()
    {
        _questManager = GetComponent<QuestManager>();
    }

    private void Update()
    {
        if (_isAvaiableToTakeQuest)
            if (_avaiableQuest != null)
                if (Input.GetKeyDown(KeyCode.Y) && !_avaiableQuest.IsQuestComplete)
                    if (_avaiableQuest.IsQuestActive == false)
                    {
                        _questManager.AddQuestToQuestList(_avaiableQuest.Quests[0]);
                        _avaiableQuest.IsQuestActive = true;
                    }
                    else
                    {
                        _questManager.CompleteQuest(_avaiableQuest.Quests[0]);
                        _avaiableQuest.CompleteQuest();
                    }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<NPC>())
        {
            _avaiableQuest = collision.gameObject.GetComponent<NPC>();

            _isAvaiableToTakeQuest = true;

            Debug.Log("Can Take quest");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<NPC>())
        {
            _avaiableQuest = null;

            _isAvaiableToTakeQuest = false;
        }
    }
}
