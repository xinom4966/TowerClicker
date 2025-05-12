using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreDisplay;
    private int lastLevelInd;
    [SerializeField] private MenuType menuType;

    private void Start()
    {
        Time.timeScale = 1.0f;
        if (menuType == MenuType.LoseMenu)
        {
            List<int> scores = ScoreManager.Instance.GetScores();
            for (int i = 0; i < 10; i++)
            {
                if (i < scores.Count)
                {
                    scoreDisplay.text += "\n" + scores[i];
                }
            }
            lastLevelInd = ScoreManager.Instance.GetLastRegisteredInd();
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
        SceneManager.LoadScene(lastLevelInd);
    }

    enum MenuType
    {
        MainMenu,
        LoseMenu
    }
}
