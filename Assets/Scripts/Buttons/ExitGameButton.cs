using UnityEngine;

/// <summary>
/// ����� �� ����
/// </summary>
public class ExitGameButton : ButtonBase
{
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
        Application.Quit();
    }

    protected override void LoadSubMenu()
    {
        throw new System.NotImplementedException();
    }
}
