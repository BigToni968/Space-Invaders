using System.Collections.Generic;
using UnityEngine;
using System;

public class Wave : WaveBase
{
    [SerializeField] private WaveData _waveData = null;

    public override event Action<WaveBase> WaveIsCleaned = delegate { };
    protected override WaveData WaveData { get => _waveData; set => _waveData = value; }
    protected override List<UnarmedController> Controllers { get; set; }

    public override void Initializing()
    {
        base.Initializing();
        Controllers = new List<UnarmedController>(WaveData.Count);
    }

    public override void Instantiate()
    {
        base.Instantiate();
        for (int i = 0; i < Controllers.Capacity; i++)
            Controllers.Add(Instantiate(WaveData.Controller, transform));

        CorrectionPositionShip();
    }

    public virtual void CorrectionPositionShip()
    {
        float positionCenterCamera = Camera.main.transform.position.x - Camera.main.Rectangle().Widht;

        for (int i = 0; i < Controllers.Count; i++)
        {
            Vector2 position = Controllers[i].transform.position;

            position.x -= Controllers.Count;
            position.x += (i + 1) * Controllers[i].SpaceShip.transform.localScale.x / 2;

            Controllers[i].SpaceShip.transform.position = position;
        }
    }

    public override void Clear()
    {
        base.Clear();
        for (int i = 0; i < Controllers.Count; i++)
            Destroy(Controllers[i].gameObject);

        Controllers.Clear();
    }

    protected virtual void CalculatingShipDestroy(SpaceShip SpaceShip)
    {
        int sumDestroydShip = 0;
        foreach (Controller controller in Controllers)
            if (controller.SpaceShip == null) sumDestroydShip++;

        if (sumDestroydShip == Controllers.Count - 1)
        {
            Clear();
            WaveIsCleaned?.Invoke(this);
        }
    }
}
