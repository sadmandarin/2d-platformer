using UnityEngine;

public class EnterReleaseQuestTrigger : MonoBehaviour
{
    private HostageReleaseQuest _quest;

    public void Init(HostageReleaseQuest quest)
    {
        _quest = quest;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            StartCoroutine(_quest.QuestTimer());
        }
    }
}
