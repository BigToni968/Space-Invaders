using UnityEngine;

public class Timer
{
    private float _timePassed = 0;
    public bool Counting(float TimeDelay)
    {
        if (_timePassed > TimeDelay) return !System.Convert.ToBoolean(_timePassed = 0);
        else _timePassed += Time.deltaTime;
        return false;
    }
}
