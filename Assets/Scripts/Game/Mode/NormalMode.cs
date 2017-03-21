using UnityEngine;
using System.Collections;
using System;

public class NormalMode : GameModeBase
{
    public override IEnumerator GameLoop()
    {
        yield return 0;
    }

    public override void GameOver()
    {
    }

    public override void GamePause()
    {
    }

    public override void GameResume()
    {
    }

    public override void Init()
    {
    }

    public override void RestartGame()
    {
    }
}
