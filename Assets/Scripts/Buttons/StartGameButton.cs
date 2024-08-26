using UnityEngine.SceneManagement;

public class StartGameButton : ButtonBase
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void OnClick()
    {
        SceneManager.LoadScene(1);
    }
}
