using UnityEngine;

// Creating a separate folder for a single script might be a bit of an overkill
public class Fences : MonoBehaviour // "Fences" seems like a wrong word. Did you mean "borders"?
{
    [Header("Important field")]
    [Tooltip("Wave manager data")]
    [SerializeField] private WaveManagerData _waveManagerData = null;
    [SerializeField] private BoxCollider2D _leftWall, _rightWall;
    [SerializeField] private SpriteRenderer _playArea = null;

    private float WidthCamera { get; set; }
    private float HeightCamera { get; set; }
    private Camera Camera { get; set; }

    // I added some whitespaces and newlines to this method for better readability, please take notice
    private void Awake()
    {
        // Please never use FindObjectOfType. Not only is it slow, but it can also make debugging very difficult
        // use [SerializeField] or at most GetComponent (but only when accessing objects dynamically)
        Camera = FindObjectOfType<Camera>(); 
        
        if (_leftWall == null || _rightWall == null) 
            (_leftWall, _rightWall) = GenerateWall();
        
        CustomizeWall();
        
        if (_playArea == null) 
            _playArea = GeneratePlayAreae();
        
        CustomizePlayArea();
    }

    // I added some whitespaces and newlines to this method for better readability, please take notice
    private (BoxCollider2D, BoxCollider2D) GenerateWall()
    {
        // Spawning objects at runtime is better avoided when possible. It saves us from writing extra code
        // and makes debugging easier.
        // Consider adding these objects to scene manually and passing to this script using [SerializeField]
        BoxCollider2D leftWwall = new GameObject("Wall").AddComponent<BoxCollider2D>();
        BoxCollider2D rightWall = Instantiate(leftWwall, transform);
        
        rightWall.name = leftWwall.name;
        rightWall.isTrigger = leftWwall.isTrigger = true;
        
        leftWwall.transform.parent = transform;
        leftWwall.transform.position = Vector3.zero;
        
        return (leftWwall, rightWall);
    }

    // Seems like a poor name choice for a method. "Customize" is too broad of a term.
    private void CustomizeWall()
    {
        // Good uses of newlines in this method, however "//" is generally not used to do that. A simple empty line is enough.
        
        // Having multiple assignments in the same line is usually bad fo readability.
        // It's ok to use
        //   _leftWall.tag = _leftWall.name;
        //   _rightWall.tag = _leftWall.name;
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

    // I suggest paying closer attention to typos
    // They don't break anything, but they do make the code feel a bit more clumsy
    private SpriteRenderer GeneratePlayAreae()
    {
        // Spawning objects at runtime is better avoided when possible. It saves us from writing extra code
        // and makes debugging easier.
        // Especially in this case. It appears like you are creating quite a complex object. It's better to create it as prefab
        // and just spawn it.
        SpriteRenderer gameZone = new GameObject("PlayArea").AddComponent<SpriteRenderer>();
        gameZone.transform.parent = transform;
        Texture2D texture = new Texture2D(256, 256);
        Sprite sprite = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(.5f, .5f), 256f);
        gameZone.sprite = sprite;
        sprite = null;
        return gameZone;
    }

    // Seems like a poor name choice for a method. "Customize" is too broad of a term.
    private void CustomizePlayArea()
    {
        _playArea.tag = _playArea.name;
        _playArea.sortingOrder = -1;
        _playArea.gameObject.AddComponent<BoxCollider2D>().isTrigger = true;
        _playArea.color = Color.black;
        _playArea.transform.localScale = new Vector3(WidthCamera * 2, HeightCamera * 2, 0);
    }
}
