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

    public void ButtonResetGame()
    {
        NumberDeathEnemies = 0;
        DeathEnemiesText.text = "0";

        MonoBehaviour[] allObjects = FindObjectsOfType<MonoBehaviour>();
        MusicManager.Instance.ResetAllAudioSource();
        foreach (MonoBehaviour _gameObject in allObjects)
        {
            /*if (_gameObject.gameObject.layer == LayerMask.NameToLayer("Enemy")
                || _gameObject.gameObject.layer == LayerMask.NameToLayer("Bullet")
                || _gameObject.gameObject.layer == LayerMask.NameToLayer("Chest")
                || _gameObject.gameObject.layer == LayerMask.NameToLayer("Weapon"))
            {
                Destroy(_gameObject);
            }*/
            Destroy(_gameObject);
        }

        /*PlayerController.ResetInstance();
        LevelManager.ResetInstance();
        UpgradeManager.ResetInstance();
        PoolingChest.ResetInstance();
        SpawnInCircle.ResetInstance();
        PoolingBullet.ResetInstance();
        PoolingItem.ResetInstance();
        SpawnEnemyAroundPlayer.ResetInstance();
        SpellManager.ResetInstance();
        MusicManager.ResetInstance();*/

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        Time.timeScale = 1f;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
