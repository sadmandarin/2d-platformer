using UnityEngine;

/// <summary>
/// ����� ������ � ���� ��������� ������
/// </summary>
/// <remarks>
/// ������ ��������������� ��������������� ����� � SO � ����������������
///</remarks>
public class ItemEquip : ButtonBase
{
    [SerializeField] protected PlayerSettingsSO _playerSettingsSO;
    private ScrollViewManager _scrollViewManager;

    private void OnEnable()
    {
        _scrollViewManager = FindFirstObjectByType<ScrollViewManager>().GetComponent<ScrollViewManager>();
    }

    protected override void Update()
    {
        base.Update();

        if (_isSelected)
        {
            Navigate();

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SubMenuExit();
            }
        }
    }

    
    protected override void DoAction()
    {
        var item = GetComponent<ItemView>();

        if (item.TypeOfItem is WeaponsBase)
            _playerSettingsSO.SetHaracterictic(item.TypeOfItem as WeaponsBase);
        else if (item.TypeOfItem is PlayerArmorBase)
            _playerSettingsSO.SetHaracterictic(item.TypeOfItem as PlayerArmorBase);
        else if (item.TypeOfItem is AbilitySOBase)
            _playerSettingsSO.SetHaracterictic(item.TypeOfItem as AbilitySOBase);

        SubMenuExit();
    }

    /// <summary>
    /// ����� �� �������
    /// </summary>
    void SubMenuExit()
    {
        Deselect();

        GetComponentInParent<EquipMenu>().SelectButton();

        _scrollViewManager.gameObject.SetActive(false);
    }

    protected override void LoadSubMenu()
    {
        throw new System.NotImplementedException();
    }
}
