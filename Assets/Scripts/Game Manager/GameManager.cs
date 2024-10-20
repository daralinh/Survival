using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public Victory Victory;
    public Lose Lose;
    public TextMeshProUGUI DeathEnemiesText;
    public Button StartGameButton;

    public GameObject UIStart;

    public int NumberDeathEnemies { get; private set; }

    public bool IsVictory { get; private set; }
    public bool IsLose { get; private set; }

    private float originTime;

    protected override void Awake()
    {
        base.Awake();
        IsLose = false;
        IsVictory = false;
        NumberDeathEnemies = 0;
        DeathEnemiesText.text = "0";
    }

    private void Start()
    {
        UIStart.gameObject.SetActive(true);
        Pause();
    }

    public void IncNumberDeathEnemies()
    {
        DeathEnemiesText.text = $"{++NumberDeathEnemies}";
    }

    public void WinGame()
    {
        IsVictory = true;
        Pause();
        Victory.Born();
    }

    public void Pause()
    {
        MusicManager.Instance.SpawnDummySource.Pause();

        if (Time.timeScale == 0)
        {
            return;
        }

        originTime = Time.timeScale;
        Time.timeScale = 0;
    }

    public void Continuous()
    {
        Time.timeScale = originTime;
        MusicManager.Instance.SpawnDummySource.Play();
    }

    public void LoseGame()
    {
        IsLose = true;
        Pause();
        Lose.Born();
    }

    public void ButtonStartGame()
    {
        UIStart.gameObject.SetActive(false);
        GameManager.Instance.Continuous();
        CursorManager.Instance.SetCursor1();
    }

    public void ButtonResetGame()
    {
        NumberDeathEnemies = 0;
        DeathEnemiesText.text = "0";

        MonoBehaviour[] allObjects = FindObjectsOfType<MonoBehaviour>();
        MusicManager.Instance.ResetAllAudioSource();
        foreach (MonoBehaviour script in allObjects)
        {
            /*if (_gameObject.gameObject.layer == LayerMask.NameToLayer("Enemy")
                || _gameObject.gameObject.layer == LayerMask.NameToLayer("Bullet")
                || _gameObject.gameObject.layer == LayerMask.NameToLayer("Chest")
                || _gameObject.gameObject.layer == LayerMask.NameToLayer("Weapon"))
            {
                Destroy(_gameObject);
            }*/
            Destroy(script.gameObject);
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
