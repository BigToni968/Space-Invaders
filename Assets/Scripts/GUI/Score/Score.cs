using UnityEngine;
using TMPro;

public class Score : GUIElement
{
    [SerializeField] private TextMeshProUGUI _outputScore = null;

    private float _life { get; set; }
    private float _score { get; set; }

    // Please don't use GetComponent when it's possible to use SerializeField
    private void Awake() => _outputScore = GetComponentInChildren<TextMeshProUGUI>();

    public void ResetValue()
    {
        ResetValue(ScoreValue.Life);
        ResetValue(ScoreValue.Score);
    }

    public void ResetValue(ScoreValue Variable, float Value)
    {
        // why not Variable == ScoreValue.Life o_0
        if (Variable.Equals(ScoreValue.Life)) _life = Value;
        if (Variable.Equals(ScoreValue.Score)) _score = Value;
        OuputScore();
    }

    public void ResetValue(ScoreValue Variable) => ResetValue(Variable, 0f);

    public void Add(ScoreValue Variable, float Value)
    {
        // why not Variable == ScoreValue.Life o_0
        if (Variable.Equals(ScoreValue.Life)) _life += Value;
        if (Variable.Equals(ScoreValue.Score)) _score += Value;
        if (_life < 0) _life = 0;
        if (_score < 0) _score = 0;
        OuputScore();
    }

    private void OuputScore() => _outputScore.text = $" Life : {_life}\n Score : {_score}";
}
