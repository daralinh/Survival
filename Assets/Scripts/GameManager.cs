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
        Victory.Born();
    }

    public void LoseGame()
    {
        Lose.Born();
    }

    public void ResetGame()
    {
        NumberDeathEnemies = 0;
        DeathEnemiesText.text = "0";

        Singleton<PlayerController>.ResetInstance();
        Singleton<LevelManager>.ResetInstance();
        Singleton<UpgradeManager>.ResetInstance();
        Singleton<PoolingChest>.ResetInstance();
        Singleton<SpawnInCircle>.ResetInstance();
        Singleton<PoolingBullet>.ResetInstance();
        Singleton<PoolingItem>.ResetInstance();
        Singleton<SpawnEnemyAroundPlayer>.ResetInstance();
        Singleton<SpellManager>.ResetInstance();
        Singleton<MusicManager>.ResetInstance();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
