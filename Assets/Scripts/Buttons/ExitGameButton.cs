using UnityEngine;

/// <summary>
/// Выход из игры
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
