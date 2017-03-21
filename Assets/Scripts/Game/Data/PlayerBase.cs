using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerBase : MonoBehaviour
{

    public enum PlayerType
    {
        Normal  = 1,
        Robot   = 2,
        PVP     = 3        
    }

    public delegate void GetScoreDelegate(int addScore);

    public delegate void ChainDelegate(int chainCount,Vector3 pos);

    public delegate void GameOverDelegate();

    //得分事件回调
    public event GetScoreDelegate OnGetScore = null;

    //连消事件回调
    public event ChainDelegate OnChain = null;

    //游戏结束事件回调
    public event GameOverDelegate OnGameOver = null;

    //玩家名称
    public string Name = "";

    //分数
    public int Score = 0;

    //游戏总时间
    public float TotalGameTime = 0;

    //方块地图容器
    public SquareSprite[,] SquareMap = null;

    //缓存行数目，简化代码
    protected int row;

    //缓存列数目，简化代码
    protected int column;

    //方块节点缓存
    private Transform squareRoot;

    //0行0列的方块位置
    private Vector3 startPos;

    //地图偏移
    private Vector3 mapOffset = Vector3.zero;

    //正在移除的方块数据
    private List<RemoveData> removingDataList = new List<RemoveData>();

    //是否是机器人判断
    public bool IsRobot { get { return isRobot; } }
    protected bool isRobot = false;

    //是否游戏结束
    public bool IsGameOver { get { return gameOver; } }
    protected bool gameOver = false;

    //创建Player对应的GameObject
    public static PlayerBase CreatePlayer(PlayerType playerType,Transform root)
    {
        GameObject playerGo = new GameObject(playerType.ToString());

        PlayerBase player = playerGo.AddComponent<PlayerBase>();

        playerGo.transform.SetParent(root);
        playerGo.transform.localPosition = Vector3.zero;
        playerGo.transform.localScale = Vector3.one;
        playerGo.layer = root.gameObject.layer;

        return player;
    }

    //初始化
    public virtual void InitPlayerMap(int[,] map)
    {
        row = map.GetLength(0);
        column = map.GetLength(1);
        startPos = new Vector3(-column * GameSetting.SquareWidth / 2f + GameSetting.SquareWidth / 2, row * GameSetting.SquareWidth / 2 - GameSetting.SquareWidth / 2, 0);

        SquareMap = new SquareSprite[row, column];

        squareRoot = transform;

        squareRoot.localPosition = mapOffset;

        for (int r = 0; r < row; r++)
        {
            for (int c = 0; c < column; c++)
            {
                if (map[r, c] != 0)
                {
                    Vector3 pos = GetPos(r, c);
                    SquareSprite ss = SquareSprite.CreateSquare(map[r, c], r, c);
                    ss.transform.SetParent(squareRoot);
                    ss.transform.localPosition = pos;
                    ss.name = "Rect[" + r + "," + c + "]";
                    SquareMap[r, c] = ss;
                    SquareMap[r, c].gameObject.layer = squareRoot.gameObject.layer;
                }
                else
                {
                    SquareMap[r, c] = null;
                }
            }
        }
    }

    //初始化地图遮罩
    public void InitMapMask()
    {
        Sprite sprite = Resources.Load<Sprite>("Texture/Game/fk1");

        GameObject mask = new GameObject(Name + "MapMask");
        mask.gameObject.layer = gameObject.layer;
        mask.transform.SetParent(transform.parent);
        mask.transform.localPosition = mapOffset;

        SpriteRenderer sr = mask.AddComponent<SpriteRenderer>();
        sr.sprite = sprite;
        sr.material = Resources.Load<Material>("Materials/SpriteStencilMask");
        sr.sortingLayerName = "Game";
        sr.sortingOrder = 1;

        float mapWidth = GameSetting.SquareWidth * column * 100;
        float mapHeight = GameSetting.SquareWidth * row * 100;

        //计算遮罩的Sprite 缩放宽，高
        float xScale = mapWidth / sprite.rect.size.x;
        float yScale = mapHeight / sprite.rect.size.y;

        mask.transform.localScale = new Vector3(xScale, yScale, 0);
    }


    //更新所有方块和障碍方块的状态
    protected virtual void UpdateState()
    {
        for (int r = row - 1; r >= 0; r--)
        {
            for (int c = 0; c < column; c++)
            {
                if (SquareMap[r, c] != null)
                {
                    SquareMap[r, c].UpdateState();
                }
            }
        }
    }

    //更新逻辑
    public virtual void PlayerUpdate()
    {
        UpdateState();
    }

    public void SetMapPos(Vector3 pos)
    {
        this.mapOffset = pos;
    }

    private Vector3 GetPos(int r, int c)
    {
        return startPos + new Vector3(c * GameSetting.SquareWidth, -r * GameSetting.SquareWidth, 0);
    }
}



