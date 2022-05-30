using System.Collections.Generic;
using UnityEngine;
using System;

public class WaveManager : MonoBehaviour
{
    // None of this classes fields need assignments of default values.
    [SerializeField] private WaveManagerData _waveManagerData = null;

    public event Action CleanWaves = delegate { };

    private List<WaveUnarmed> _waveList = null;
    // not sure we need this field, it's a bit confusing
    // let's discuss it on Discord
    private int _sumCalculationCompleteWaves = 0;
    private Camera _camera = null;

    private void Awake()
    {
        // Please never use Find
        _camera = FindObjectOfType<Camera>();

        Initializing();
        InstantiateRegimeWise();
    }
    private void Initializing() => _waveList = new List<WaveUnarmed>(_waveManagerData.Waves);

    private void InstantiateAll()
    {
        for (int i = 0; i < _waveList.Count; i++)
        {
            _waveList[i] = Instantiate(_waveList[i], transform);
            // Usually position is shortened to pos, not poss
            Vector2 poss = _waveList[i].transform.position;

            // You have the same logic in 2 methods. Never copypaste code, you should move it into a separate method
            // Please read on DRY (Don't Repeat Yourself) principle
            poss.y = _camera.transform.position.y + _camera.Rectangle().Height;
            poss.y -= _waveList[i].transform.localScale.y / 2;

            poss.y += i * _waveManagerData.DistanceBetween;
            _waveList[i].transform.position = poss;
        }
    }

    // The method name seems wrong, you don't instantiate waves one by one, "InstantiateSingleWave" would be more appropriate
    private void InstantiateOneByOne()
    {
        _waveList[_sumCalculationCompleteWaves] = Instantiate(_waveList[_sumCalculationCompleteWaves], transform);
        Vector2 poss = _waveList[_sumCalculationCompleteWaves].transform.position;

        poss.y = _camera.transform.position.y + _camera.Rectangle().Height;
        poss.y -= _waveList[_sumCalculationCompleteWaves].transform.localScale.y / 2;

        _waveList[_sumCalculationCompleteWaves].transform.position = poss;

        if (_sumCalculationCompleteWaves > 0)
        {
            UnSunbscriptionWeawCleaned();
            SubscriptionWeawCleaned();
        }
    }

    protected void InstantiateRegimeWise()
    {
        if (_waveManagerData.ModeInstantiateWave == ModeInstantiateWave.All) InstantiateAll();
        // this could be replaced by "else"
        if (_waveManagerData.ModeInstantiateWave == ModeInstantiateWave.OneByOne) InstantiateOneByOne(); 
    }

    // It's better to not use Nouns for method names, better use Verbs, e.g. SubscribeToWaveCleaned
    private void SubscriptionWeawCleaned() // *wave
    {
        foreach (WaveUnarmed wave in _waveList)
            wave.WaveIsCleaned += CalculationCompleteWaves;
    }

    // check notes to previous method
    private void UnSunbscriptionWeawCleaned()
    {
        foreach (WaveUnarmed wave in _waveList)
            wave.WaveIsCleaned -= CalculationCompleteWaves;
    }

    // Use verbs for method names, e.g. CalculateAllWavesCompleted
    private void CalculationCompleteWaves(WaveBase Wave)
    {
        Debug.Log($"Clean up Wave {Wave.GetType()}");
        Destroy(Wave.gameObject);
        _sumCalculationCompleteWaves++;
        if (_sumCalculationCompleteWaves == _waveList.Count)
        {
            _waveList.Clear();
            CleanWaves?.Invoke();
            return;
        }

        if (_waveManagerData.ModeInstantiateWave == ModeInstantiateWave.OneByOne) InstantiateOneByOne();
    }

    private void OnEnable() => SubscriptionWeawCleaned();

    private void OnDisable() => UnSunbscriptionWeawCleaned();
    private void OnDestroy() => OnDisable();

    public void OnUpdate()
    {
        for (int i = 0; i < _waveList.Count; i++)
            _waveList[i].OnUpdate();
    }
}
