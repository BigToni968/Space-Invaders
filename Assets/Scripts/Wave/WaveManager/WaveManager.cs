using System.Collections.Generic;
using UnityEngine;
using System;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private WaveManagerData _waveManagerData = null;

    public event Action CleanWaves = delegate { };

    private List<WaveUnarmed> _waveList = null;
    private int _sumCalculationCompleteWaves = 0;
    private Camera _camera = null;

    private void Awake()
    {
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
            Vector2 poss = _waveList[i].transform.position;

            poss.y = _camera.transform.position.y + _camera.Rectangle().Height;
            poss.y -= _waveList[i].transform.localScale.y / 2;

            poss.y += i * _waveManagerData.DistanceBetween;
            _waveList[i].transform.position = poss;
        }
    }

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
        if (_waveManagerData.ModeInstantiateWave == ModeInstantiateWave.OneByOne) InstantiateOneByOne();
    }

    private void SubscriptionWeawCleaned()
    {
        foreach (WaveUnarmed wave in _waveList)
            wave.WaveIsCleaned += CalculationCompleteWaves;
    }

    private void UnSunbscriptionWeawCleaned()
    {
        foreach (WaveUnarmed wave in _waveList)
            wave.WaveIsCleaned -= CalculationCompleteWaves;
    }

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
