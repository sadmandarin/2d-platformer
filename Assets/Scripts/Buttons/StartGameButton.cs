using UnityEngine.SceneManagement;

/// <summary>
/// ������ ������ ���� � ������� ����
/// </summary>
public class StartGameButton : ButtonBase
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
