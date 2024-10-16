using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public Victory Victory;
    public Lose Lose;
    public TextMeshProUGUI DeathEnemiesText;

    public int NumberDeathEnemies { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        NumberDeathEnemies = 0;
        DeathEnemiesText.text = "0";
    }

    public void IncNumberDeathEnemies()
    {
        DeathEnemiesText.text = $"{++NumberDeathEnemies}";
    }

    public void WinGame()
    {
        Time.timeScale = 0;
        Victory.Born();
    }

    public void LoseGame()
    {
        Lose.Born();
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
