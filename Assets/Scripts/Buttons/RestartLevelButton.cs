using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevelButton : ButtonBase
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
