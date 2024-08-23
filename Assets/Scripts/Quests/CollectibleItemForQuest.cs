using UnityEngine;

public class CollectibleItemForQuest : MonoBehaviour
{
    public string ItemName;
    public Vector3 SpawnPosition;

    private void OnEnable()
    {
        transform.position = SpawnPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<QuestManager>())
        {
            collision.gameObject.GetComponent<QuestManager>().CollectItem(ItemName);

            Destroy(gameObject);
        }

    }
}
