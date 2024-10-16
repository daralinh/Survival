using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public Victory Victory;
    public Lose Lose;
    public TextMeshProUGUI DeathEnemiesText;
    public Button StartGameButton;

    public int NumberDeathEnemies { get; private set; }

    public bool IsVictory { get; private set; }
    public bool IsLose { get; private set; }

    private float originTime;

    protected override void Awake()
    {
        base.Awake();
        NumberDeathEnemies = 0;
        DeathEnemiesText.text = "0";
        StartGameButton.gameObject.SetActive(true);
    }

    private void Start()
    {
        StartGameButton.gameObject.SetActive(true);
        originTime = Time.timeScale;
        Time.timeScale = 0;
    }

    public void IncNumberDeathEnemies()
    {
        DeathEnemiesText.text = $"{++NumberDeathEnemies}";
    }

    public void WinGame()
    {
        IsVictory = true;
        Victory.Born();
    }

    public void LoseGame()
    {
        IsLose = false;
        Lose.Born();
    }

    public void ButtonStartGame()
    {
        StartGameButton.gameObject.SetActive(false);
        Time.timeScale = originTime;
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
