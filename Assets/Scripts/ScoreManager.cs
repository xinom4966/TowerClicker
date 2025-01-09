using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int _levelsCount;
    private List<List<int>> _scoreBoard = new List<List<int>>();
    private int _lastRegisteredInd = 0;
    public static ScoreManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        for (int i = 0; i <= _levelsCount; i++)
        {
            _scoreBoard.Add(new List<int>());
        }
    }

    public void AddScoreToBoard(int score, int levelInd)
    {
        if (levelInd > _scoreBoard.Count)
        {
            return;
        }
        _scoreBoard[levelInd].Add(score);
        _scoreBoard[levelInd].Sort();
        _scoreBoard[levelInd].Reverse();
        _lastRegisteredInd = levelInd;
    }

    public List<int> GetScores()
    {
        return _scoreBoard[_lastRegisteredInd];
    }

    public int GetLastRegisteredInd()
    {
        return _lastRegisteredInd;
    }
}
