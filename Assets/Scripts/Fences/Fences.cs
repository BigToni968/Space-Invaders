using UnityEngine;

public class Fences : MonoBehaviour
{
    [Header("Important field")]
    [Tooltip("Wave manager data")]
    [SerializeField] private WaveManagerData _waveManagerData = null;
    [SerializeField] private BoxCollider2D _leftWall, _rightWall;
    [SerializeField] private SpriteRenderer _playArea = null;

    private float WidthCamera { get; set; }
    private float HeightCamera { get; set; }
    private Camera Camera { get; set; }

    private void Awake()
    {
        Camera = FindObjectOfType<Camera>();
        if (_leftWall == null || _rightWall == null) (_leftWall, _rightWall) = GenerateWall();
        CustomizeWall();
        if (_playArea == null) _playArea = GeneratePlayAreae();
        CustomizePlayArea();
    }

    private (BoxCollider2D, BoxCollider2D) GenerateWall()
    {
        BoxCollider2D leftWwall = new GameObject("Wall").AddComponent<BoxCollider2D>();
        BoxCollider2D rightWall = Instantiate(leftWwall, transform);
        rightWall.name = leftWwall.name;
        rightWall.isTrigger = leftWwall.isTrigger = true;
        leftWwall.transform.parent = transform;
        leftWwall.transform.position = Vector3.zero;
        return (leftWwall, rightWall);
    }

    private void CustomizeWall()
    {
        //
        _leftWall.tag = _rightWall.tag = _leftWall.name;

        //
        _leftWall.offset = new Vector2(-_leftWall.transform.localScale.z / 2, _leftWall.transform.localScale.z / 2);
        _rightWall.offset = new Vector2(_rightWall.transform.localScale.z / 2, _leftWall.transform.localScale.z / 2);

        //
        (WidthCamera, HeightCamera) = Camera.Rectangle();

        //
        Vector3 scaleWall = Vector3.zero;
        scaleWall = _leftWall.transform.localScale;
        scaleWall.y = (_waveManagerData.Waves.Count * _waveManagerData.DistanceBetween) + HeightCamera * 2;
        _leftWall.transform.localScale = _rightWall.transform.localScale = scaleWall;

        //
        _leftWall.transform.position = Camera.transform.position - new Vector3(WidthCamera, +HeightCamera, 0);
        _rightWall.transform.position = Camera.transform.position + new Vector3(WidthCamera, -HeightCamera, 0);
    }

    private SpriteRenderer GeneratePlayAreae()
    {
        SpriteRenderer gameZone = new GameObject("PlayArea").AddComponent<SpriteRenderer>();
        gameZone.transform.parent = transform;
        Texture2D texture = new Texture2D(256, 256);
        Sprite sprite = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(.5f, .5f), 256f);
        gameZone.sprite = sprite;
        sprite = null;
        return gameZone;
    }

    private void CustomizePlayArea()
    {
        _playArea.tag = _playArea.name;
        _playArea.sortingOrder = -1;
        _playArea.gameObject.AddComponent<BoxCollider2D>().isTrigger = true;
        _playArea.color = Color.black;
        _playArea.transform.localScale = new Vector3(WidthCamera * 2, HeightCamera * 2, 0);
    }
}
