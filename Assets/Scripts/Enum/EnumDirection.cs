using UnityEngine;

public static class GetDirection
{
    public static Vector2 GetVector(EnumDirection Direction)
    {
        Vector2 result = Vector2.zero;
        if (Direction.Equals(EnumDirection.Up)) result = Vector2.up;
        if (Direction.Equals(EnumDirection.Down)) result = Vector2.down;
        if (Direction.Equals(EnumDirection.Lef)) result = Vector2.left;
        if (Direction.Equals(EnumDirection.Right)) result = Vector2.right;
        return result;
    }
}

public enum EnumDirection
{
    Zero,
    Up,
    Down,
    Lef,
    Right
}