using UnityEngine;

/// <summary>
/// ������ � ���� ������ ������, ������� �������
/// </summary>
public class EquipMenu : ButtonBase
{
    [SerializeField] protected GameObject _subMenu;

    protected override void Update()
    {
        base.Update();

        if (_isSelected)
        {
            Navigate();
        }
    }

    protected override void DoAction()
    {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// �������� �������
    /// </summary>
    protected override void LoadSubMenu()
    {
        _subMenu.SetActive(true);
        Deselect();
    }
}
