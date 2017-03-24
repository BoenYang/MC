using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using DG.Tweening;
using UnityEngine;

public class SquareSprite : MonoBehaviour
{
    public delegate void MoveEndCallBack();

    public delegate void MoveNextNullCallBack(SquareSprite square);

    public int Row;

    public int Column;

    public int Type;

    public SquareState State;

    public bool Chain;

    public SpriteRenderer Renderer;

    public PlayerBase Player;

	public bool Visited = false;

	public List<SquareSprite> connectedSquare;

    private static Dictionary<int, string> effectDict = new Dictionary<int, string>()
    {
        { 1,"green_effect"},
        { 2,"purple_effect"},
        { 3,"orange_effect"},
        { 4,"yellow_effect"},
        { 5,"blue_effect"},
        { 6,"red_effect"},
        { 7,"brown_effect"},
    };


    public bool IsAnimating
    {
        get { return isAnimating; }
    }

    private bool isAnimating = false;

    public int targetRow;

    public Vector3 targetPos;

    public static SquareSprite CreateSquare(int type, int r, int c)
    {
        GameObject go = new GameObject();
        SquareSprite ss = go.AddComponent<SquareSprite>();
        SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
        BoxCollider2D collider = go.AddComponent<BoxCollider2D>();


        sr.sortingLayerName = "Game";
        sr.sortingOrder = 3;
        sr.sprite = Resources.Load<Sprite>("Texture/Game/sg" + type);

        //sr.material = Resources.Load<Material>("Materials/SpriteWithStencil");

        ss.Column = c;
        ss.Row = r;
        ss.Type = type;
        ss.Renderer = sr;
        ss.State = SquareState.Static;

        collider.size = new Vector2(0.7f, 0.7f);

        go.transform.localScale = Vector3.one * 0.8f;

        return ss;
    }

    public void OnMouseUpAsButton()
    {
		if (connectedSquare == null) {
			Player.SquareSpriteClick (this);
		} else {
			Player.RemoveConnectedSquare (this);
		}
    }

    public void UpdateState()
    {
        SquareSprite[,] squareMap = Player.SquareMap;

        switch (State)
        {
            case SquareState.Static:
              
                if (Row < squareMap.GetLength(0) - 1)
                {
                    SquareSprite down = squareMap[Row + 1, Column];
                    if (down == null)
                    {
                        State = SquareState.Hung;
                    }
                    else
                    {
                        State = down.State;
                    }
                }
                else
                {
                    State = SquareState.Static;
                }
                break;
            case SquareState.Hung:
                Fall();
                State = SquareState.Fall;
                break;
        }
    }

    private void Fall()
    {
        SquareSprite[,] squareMap = Player.SquareMap;
        int downNullCount = 0;
        if (Row < squareMap.GetLength(0) - 1)
        {
            for (int r = Row + 1; r < squareMap.GetLength(0); r++)
            {
                if (squareMap[r, Column] == null)
                {
                    downNullCount++;
                }
            }

        }

        if (downNullCount > 0)
        {
            targetRow = Row + downNullCount;
            targetPos = Player.GetPos(targetRow, Column);

            squareMap[Row, Column] = null;
            squareMap[targetRow, Column] = this;
            Row = targetRow;
            transform.DOLocalMove(targetPos, 0.1f).SetEase(Ease.Linear).OnComplete(() =>
            {
                State = SquareState.Static;
            });
        }
    }

}

