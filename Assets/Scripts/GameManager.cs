using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _numberToKillBeforeSpeedUp;
    [SerializeField] private UnityEvent _SpeedUpEvent;
    [SerializeField] private TextMeshProUGUI _scoreDisplay;
    private int _counter = 0;
    private int _score = 0;

    public void OnEnnemyKilled()
    {
        _counter++;
        _score++;
        if (_counter >= _numberToKillBeforeSpeedUp)
        {
            _SpeedUpEvent.Invoke();
            _counter = 0;
            _numberToKillBeforeSpeedUp += 5;
        }
        _scoreDisplay.text = "score : " + _score;
    }
}
