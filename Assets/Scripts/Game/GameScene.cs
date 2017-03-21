
using UnityEngine;

public class GameScene : MonoBehaviour
{

    public GameMode Mode = GameMode.Normal;

    public GameModeBase Game;

    public static GameScene Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        Instance = this;
        Game = GameModeBase.CreateGameMode(Mode);
        Game.Init();
        StartCoroutine(Game.GameLoop());
    }

    public void StopGame()
    {
        StopAllCoroutines();
    }
}
