using UnityEngine;

/// <summary>
/// Квест на нахождение предмета
/// </summary>
[CreateAssetMenu(fileName = "PickUpQuest", menuName = "ScriptableObjects/Quests/PickUpQuest", order = 1)]
public class PickUpItemsQuests : QuestsBase
{
    public GameObject ObjectToSpawn;
    public bool IsItemPickUp;
    public string RequiredItemName;

    
    public override void CompleteQuest()
    {
        IsCompleted = true;
    }

    public override void StartQuest()
    {
        IsQuestActiveNow = true;

        var item = Instantiate(ObjectToSpawn);

        item.GetComponent<CollectibleItemForQuest>().ItemName = RequiredItemName;

        Debug.Log("Quest is now running");
    }

    /// <summary>
    /// Метод, вызываемый при сборе предмета
    /// </summary>
    /// <param name="itemName"></param>
    public void CollectItem(string itemName)
    {
        if (itemName == RequiredItemName)
        {
            IsItemPickUp = true;

            Debug.Log("Pick");
        }
    }
}
