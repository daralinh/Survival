using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicBook : MonoBehaviour
{
    private RectTransform rectTransform;

    public Image image;
    public Sprite[] blueSpriteArray;
    public Sprite[] redSpriteArray;
    public float speedSlide;
    public Button leftButton;
    public Button rightButton;
    public Text leftText;
    public Text rightText;

    public List<AUpgrade> normalUpgradeList = new List<AUpgrade>();
    public List<AUpgrade> specialUpgradeList = new List<AUpgrade>();

    private bool isBlue;
    private AUpgrade leftUpgrade;
    private AUpgrade rightUpgrade;
    private float originalTimeScale;

    private void Awake()
    {
        isBlue = true;
        rectTransform = image.GetComponent<RectTransform>();

        normalUpgradeList.Add(new IncAttackSpeedUpgrade());
        normalUpgradeList.Add(new IncDMGUpgrade());
        normalUpgradeList.Add(new IncOriginHpUpgrade());
        normalUpgradeList.Add(new IncRateDropChestUpgrade());

        specialUpgradeList.Add(new AddFireBallSpellUpgrade());
        specialUpgradeList.Add(new AddFireStepSpellUpgrade());
        specialUpgradeList.Add(new AddFireStormSpellUpgrade());
        specialUpgradeList.Add(new AddKunaiSpellUpgrade());
        specialUpgradeList.Add(new AddLightningSpellUpgrade());
        specialUpgradeList.Add(new DecCoolDownSpellUpgrade());
    }

    private void Start()
    {
        HideButton();
        gameObject.SetActive(false);
    }

    public void OpenBlueBook()
    {
        CursorManager.Instance.SetCursor2();
        MusicManager.Instance.PlayMagicBookSound(EMusic.OpenBlueBook);
        originalTimeScale = Time.timeScale;
        Time.timeScale = 0;
        isBlue = true;
        gameObject.SetActive(true);
        image.sprite = blueSpriteArray[0];
        StartCoroutine(PlayOpenBlueAnimUIHanlder());
    }

    public void OpenRedBook()
    {
        CursorManager.Instance.SetCursor2();
        MusicManager.Instance.PlayMagicBookSound(EMusic.OpenRedBook);
        originalTimeScale = Time.timeScale;
        Time.timeScale = 0;
        isBlue = false;
        gameObject.SetActive(true);
        image.sprite = redSpriteArray[0];
        StartCoroutine(PlayOpenRedAnimUIHanlder());
    }

    private void ShowButton()
    {
        if (isBlue)
        {
            FisherYatesShufflerAlgorithm.Shuffle(normalUpgradeList);
            leftUpgrade = normalUpgradeList[0];
            rightUpgrade = normalUpgradeList[1];
        }
        else
        {
            FisherYatesShufflerAlgorithm.Shuffle(specialUpgradeList);
            leftUpgrade = specialUpgradeList[0];
            rightUpgrade = specialUpgradeList[1];
        }

        leftButton.gameObject.SetActive(true);
        rightButton.gameObject.SetActive(true);

        leftText.text = leftUpgrade.GetContent();
        rightText.text = rightUpgrade.GetContent();
        //Debug.Log(leftText.text + " " + rightText.text);
    }

    public void ClickOnLeft()
    {
        Time.timeScale = originalTimeScale;
        HideButton();
        leftUpgrade.Active();
        CloseBook();
    }

    public void ClickOnRight()
    {
        Time.timeScale = originalTimeScale;
        HideButton();
        rightUpgrade.Active();
        CloseBook();
    }

    private void HideButton()
    {
        leftButton.gameObject.SetActive(false);
        rightButton.gameObject.SetActive(false);
    }

    public void CloseBook()
    {
        HideButton();
        MusicManager.Instance.PlayMagicBookSound(EMusic.CloseBook);

        if (isBlue)
        {
            StartCoroutine(PlayCloseBlueAnimUIHanlder());
        }
        else
        {
            StartCoroutine(PlayCloseRedAnimUIHanlder());
        }

        CursorManager.Instance.SetCursor1();
    }

    IEnumerator PlayOpenBlueAnimUIHanlder()
    {
        yield return new WaitForSecondsRealtime(0.5f);

        foreach (Sprite _sprite in blueSpriteArray)
        {
            SetPivotBySprite(_sprite);
            image.sprite = _sprite;
            yield return new WaitForSecondsRealtime(speedSlide);
        }

        ShowButton();
    }

    IEnumerator PlayOpenRedAnimUIHanlder()
    {
        yield return new WaitForSecondsRealtime(0.5f);

        foreach (Sprite _sprite in redSpriteArray)
        {
            SetPivotBySprite(_sprite);
            image.sprite = _sprite;
            yield return new WaitForSecondsRealtime(speedSlide);
        }

        ShowButton();
    }

    IEnumerator PlayCloseBlueAnimUIHanlder()
    {
        for (int i = blueSpriteArray.Length - 1; i >= 0; i--)
        {
            SetPivotBySprite(blueSpriteArray[i]);
            image.sprite = blueSpriteArray[i];
            yield return new WaitForSecondsRealtime(speedSlide);
        }

        gameObject.SetActive(false);
        yield return new WaitForSecondsRealtime(1f);
    }

    IEnumerator PlayCloseRedAnimUIHanlder()
    {
        for (int i = redSpriteArray.Length - 1; i >= 0; i--)
        {
            SetPivotBySprite(redSpriteArray[i]);
            image.sprite = redSpriteArray[i];
            yield return new WaitForSecondsRealtime(speedSlide);
        }

        gameObject.SetActive(false);
        yield return new WaitForSecondsRealtime(1f);
    }

    private void SetPivotBySprite(Sprite _sprite)
    {
        rectTransform.pivot = new Vector2(_sprite.pivot.x / _sprite.rect.width, _sprite.pivot.y / _sprite.rect.height);
    }
}
