using System.Collections.Generic;

public enum SquareState
{
    Static = 1,
    Fall = 2,
    Hung = 3,
}

public enum MoveDir
{
    Left = -1,
    Right = 1,
}

public enum RemoveDir
{
    Vertical = 1,
    Horizontal = 2,
}

public class RemoveData
{
    public List<SquareSprite> RemoveList = new List<SquareSprite>();

    public void AddHorizontalRemove(int startRow, int startColumn, int count, SquareSprite[,] map)
    {
        for (int i = 0; i < count; i++)
        {
            SquareSprite square = map[startRow, startColumn + i];
            if (!RemoveList.Contains(square))
            {
                RemoveList.Add(square);
            }
        }
    }

    public void AddVertivalRemove(int startRow, int startColumn, int count, SquareSprite[,] map)
    {
        for (int i = 0; i < count; i++)
        {
            SquareSprite square = map[startRow + i, startColumn];
            if (!RemoveList.Contains(square))
            {
                RemoveList.Add(square);
            }
        }
    }
}