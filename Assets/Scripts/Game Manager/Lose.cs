using TMPro;
using UnityEngine;

public class Lose : MonoBehaviour
{
    [SerializeField] private Button resetButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void Born()
    {
        MusicManager.Instance.ResetAllAudioSource();
        gameObject.SetActive(true);
        MusicManager.Instance.PlayNotification(EMusic.Lose);
        textMeshProUGUI.text = GameManager.Instance.NumberDeathEnemies.ToString();
    }
}
