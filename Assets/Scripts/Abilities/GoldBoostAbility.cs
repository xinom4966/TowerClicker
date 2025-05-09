using UnityEngine;

public class GoldBoostAbility : Ability
{
    [SerializeField] private Player _player;
    [SerializeField] private float _duration = 2;
    [SerializeField] private float _goldMultiplier = 1.5f;
    private int _baseValue;
    private float _timerGB = 0.0f;
    private bool _activated = false;

    protected override void Update()
    {
        if (_activated)
        {
            _timerGB += Time.deltaTime;
            if (_timerGB >= _duration)
            {
                _activated = false;
                _timerGB = 0.0f;
                CancelGoldBoost();
            }
            return;
        }
        base.Update();
    }

    public override void Execute()
    {
        base.Execute();
        if (!_usable || _activated)
        {
            return;
        }
        _baseValue = _player.GetMurderAward();
        _player.SetMurderAward(Mathf.RoundToInt(_baseValue * _goldMultiplier));
        _activated = true;
        _usable = false;
    }

    private void CancelGoldBoost()
    {
        _player.SetMurderAward(_baseValue);
    }
}
