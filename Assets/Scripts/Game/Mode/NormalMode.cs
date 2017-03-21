using UnityEngine;
using System.Collections;

public class NormalMode : GameModeBase
{
    private PlayerBase player;

    private int[,] initMap; 

    public override void Init()
    {
        GameObject mapRoot = GameObject.Find("Map");
        player = PlayerBase.CreatePlayer(PlayerBase.PlayerType.Normal,mapRoot.transform);


        initMap = new int[GameSetting.RawCount,GameSetting.ColumnCount];

        for (int r = 0; r < initMap.GetLength(0); r++)
        {
            for (int c = 0; c < initMap.GetLength(1); c++)
            {
                initMap[r, c] = Random.Range(1, GameSetting.SquareTypeCount);
            }
        }

        Debug.Log("INIT GAME");
        player.InitPlayerMap(initMap);
    }

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

    public override void RestartGame()
    {
    }
}
