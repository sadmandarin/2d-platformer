using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ����������� ������ � ���� ��������� ����������
/// </summary>
public class ItemView : MonoBehaviour
{
    public Image Image;
    public Text Text;
    public object TypeOfItem;

    /// <summary>
    /// ��������� �������� ��������� ��� �����������
    /// </summary>
    /// <param name="icon">������ ��������</param>
    /// <param name="shortDescription">������� ��������</param>
    /// <param name="type">����� ������� ������������� ������ ��� ������</param>
    public void SetUp(Sprite icon, string shortDescription, object type)
    {
        Image.sprite = icon;
        Text.text = shortDescription;
        TypeOfItem = type;

        Debug.Log(type);
    }
}
