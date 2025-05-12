using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int levelNumber;
    [SerializeField] private int numberToKillBeforeSpeedUp;
    [SerializeField] private UnityEvent SpeedUpEvent;
    [SerializeField] private TextMeshProUGUI scoreDisplay;
    [SerializeField] private ProgressGauge gauge;
    private int counter = 0;
    private int score = 0;
    private bool isFastForward = false;

    public void OnEnemyKilled()
    {
        counter++;
        score++;
        gauge.SetFillAmmount((float)counter / (float)numberToKillBeforeSpeedUp);
        if (counter >= numberToKillBeforeSpeedUp)
        {
            SpeedUpEvent.Invoke();
            counter = 0;
            numberToKillBeforeSpeedUp += 5;
        }
        scoreDisplay.text = "score : " + score;
    }

    public void OnLose()
    {
        ScoreManager.Instance.AddScoreToBoard(score, levelNumber);
    }

    public void FastForward()
    {
        isFastForward = !isFastForward;
        if (isFastForward)
        {
            Time.timeScale *= 3;
        }
        else
        {
            Time.timeScale /= 3;
        }
    }
}
