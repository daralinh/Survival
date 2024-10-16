using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Victory : MonoBehaviour
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
        gameObject.SetActive(true);
        textMeshProUGUI.text = GameManager.Instance.NumberDeathEnemies.ToString();
    }
}
