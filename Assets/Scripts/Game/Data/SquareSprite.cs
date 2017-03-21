using System.Collections.Generic;
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

    public bool HorizontalRemoved = false;

    public bool VerticalRemoved = false;

    public SpriteRenderer Renderer;

    public int UnderSameTypeSquareCount;

    public int RightSameTypeSquareCount;
    
    private static Dictionary<int,string> effectDict = new Dictionary<int, string>()
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

    private static Sprite[] sprites;

    private Vector2 mouseDownPos;

    private Vector2 mouseUpPos;

    private Color grayColor = new Color(0.3f,0.3f,0.3f,1.0f);

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

        go.transform.localScale = Vector3.one*0.8f;

        return ss;
    }

    public void OnMouseDown()
    {
        if (Row < 0)
        {
            return;
        }
    }

    public void UpdateState()
    {
       
    }
}

