using System;
using System.Collections;
using UnityEngine;

public enum GameMode
{
    Normal    = 1
}

public enum GameResult
{
    Failed    = 0,
    Win       = 1,
}

public abstract class GameModeBase
{

    public abstract void Init();

    public abstract IEnumerator GameLoop();

    public abstract void GamePause();

    public abstract void GameResume();

    public abstract void GameOver();

    public abstract void RestartGame();

    public static GameModeBase CreateGameMode(GameMode mode)
    {
        string typeName = mode.ToString() + "Mode";
        Type modeType = Type.GetType(typeName);
        if (modeType == null)
        {
            Debug.LogError("创建游戏模式失败,找不到类名为" + typeName + "的类");
            return null;
        }
        return (GameModeBase)Activator.CreateInstance(modeType);
    }

}
