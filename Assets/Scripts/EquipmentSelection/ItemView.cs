using UnityEngine;
using UnityEngine.UI;

public class ItemView : MonoBehaviour
{
    public Image Image;
    public Text Text;
    public object TypeOfItem;

    public void SetUp(Sprite icon, string shortDescription, object type)
    {
        Image.sprite = icon;
        Text.text = shortDescription;
        TypeOfItem = type;

        Debug.Log(type);
    }
}
