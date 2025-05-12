using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int levelsCount;
    private List<List<int>> scoreBoard = new List<List<int>>();
    private int lastRegisteredInd = 0;
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
        for (int i = 0; i <= levelsCount; i++)
        {
            scoreBoard.Add(new List<int>());
        }
    }

    public void AddScoreToBoard(int score, int levelInd)
    {
        if (levelInd > scoreBoard.Count)
        {
            return;
        }
        scoreBoard[levelInd].Add(score);
        scoreBoard[levelInd].Sort();
        scoreBoard[levelInd].Reverse();
        lastRegisteredInd = levelInd;
    }

    public List<int> GetScores()
    {
        return scoreBoard[lastRegisteredInd];
    }

    public int GetLastRegisteredInd()
    {
        return lastRegisteredInd;
    }
}
