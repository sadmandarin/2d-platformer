using UnityEngine;

public class ExitGameButton : ButtonBase
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void OnClick()
    {
        Application.Quit();
    }
}
