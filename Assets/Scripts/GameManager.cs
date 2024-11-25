using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _numberToKillBeforeSpeedUp;
    [SerializeField] private UnityEvent _SpeedUpEvent;
    private int _counter = 0;

    public void OnEnnemyKilled()
    {
        _counter++;
        if (_counter >= _numberToKillBeforeSpeedUp)
        {
            _SpeedUpEvent.Invoke();
            _counter = 0;
            _numberToKillBeforeSpeedUp += 5;
        }
    }
}
