using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Отображение эквипа в меню установки экипировки
/// </summary>
public class ItemView : MonoBehaviour
{
    public Image Image;
    public Text Text;
    public object TypeOfItem;

    /// <summary>
    /// Установка основных элементов для отображения
    /// </summary>
    /// <param name="icon">Иконка предмета</param>
    /// <param name="shortDescription">Краткое описание</param>
    /// <param name="type">Какой предмет устанавливает игроку при выборе</param>
    public void SetUp(Sprite icon, string shortDescription, object type)
    {
        Image.sprite = icon;
        Text.text = shortDescription;
        TypeOfItem = type;

        Debug.Log(type);
    }
}
