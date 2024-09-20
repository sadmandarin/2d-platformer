using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// перезагрузка уровня при проигрыше
/// </summary>
public class RestartLevelButton : ButtonBase
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
        SceneManager.LoadScene(1);
    }

    protected override void LoadSubMenu()
    {
        throw new System.NotImplementedException();
    }
}
