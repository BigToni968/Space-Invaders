using UnityEngine;

public static class CameraRect
{
    public static (float Widht, float Height) Rectangle(this Camera Camera) =>
        (Camera.orthographicSize * ((float)Screen.width / Screen.height), Camera.orthographicSize);
}
