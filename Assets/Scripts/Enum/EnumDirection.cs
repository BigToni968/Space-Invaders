using UnityEngine;

// It's better to use Nouns as class names, not Verbs
public static class GetDirection
{
    public static Vector2 GetVector(EnumDirection Direction) // We could have also implemented this as an extension method for EnumDirection
    {
        Vector2 result = Vector2.zero;
        if (Direction.Equals(EnumDirection.Up)) result = Vector2.up;
        if (Direction.Equals(EnumDirection.Down)) result = Vector2.down;
        if (Direction.Equals(EnumDirection.Lef)) result = Vector2.left;
        if (Direction.Equals(EnumDirection.Right)) result = Vector2.right;
        return result;
    }
}

// Are you sure we need this enum?
// Something tells me that Vector2 or Vector2Int could have done just as good, I'm not 100% sure though

public enum EnumDirection
{
    Zero,
    Up,
    Down,
    Lef,
    Right
}