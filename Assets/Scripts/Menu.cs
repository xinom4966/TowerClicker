using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class Menu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreDisplay;
    private int _lastLevelInd;
    [SerializeField] private MenuType _menuType;

    private void Start()
    {
        Time.timeScale = 1.0f;
        if (_menuType == MenuType.LoseMenu)
        {
            List<int> scores = ScoreManager.Instance.GetScores();
            for (int i = 0; i < 10; i++)
            {
                if (i < scores.Count)
                {
                    _scoreDisplay.text += "\n" + scores[i];
                }
            }
            _lastLevelInd = ScoreManager.Instance.GetLastRegisteredInd();
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

    public void Retry()
    {
        SceneManager.LoadScene(_lastLevelInd);
    }

    enum MenuType
    {
        MainMenu,
        LoseMenu
    }
}
