using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreDisplay;

    private void Start()
    {
        List<int> scores = ScoreManager.Instance.GetScores();
        foreach (int score in scores)
        {
            _scoreDisplay.text += "\n" + score;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
