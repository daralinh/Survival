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
    public ButtonForMagicBook leftButton;
    public ButtonForMagicBook rightButton;

    public List<AUpgrade> normalUpgradeList = new List<AUpgrade>();
    public List<AUpgrade> specialUpgradeList = new List<AUpgrade>();

    private bool isBlue;
    private AUpgrade leftUpgrade;
    private AUpgrade rightUpgrade;
    private float originalTimeScale;
    private Text leftText;
    private Text rightText;

    private Coroutine coroutine;

    private void Awake()
    {
        isBlue = true;
        rectTransform = image.GetComponent<RectTransform>();

        leftText = leftButton.tmp;
        rightText = rightButton.tmp;

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
        if (GameManager.Instance.IsLose || GameManager.Instance.IsVictory)
        {
            return;
        }

        //CursorManager.Instance.SetCursor2();
        MusicManager.Instance.PlayMagicBookSound(EMusic.OpenBlueBook);
        GameManager.Instance.Pause();
        isBlue = true;
        gameObject.SetActive(true);
        image.sprite = blueSpriteArray[0];

        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        coroutine = StartCoroutine(PlayOpenBlueAnimUIHanlder());
    }

    public void OpenRedBook()
    {
        if (GameManager.Instance.IsLose || GameManager.Instance.IsVictory)
        {
            return;
        }

        //CursorManager.Instance.SetCursor2();
        MusicManager.Instance.PlayMagicBookSound(EMusic.OpenRedBook);
        GameManager.Instance.Pause();
        isBlue = false;
        gameObject.SetActive(true);
        image.sprite = redSpriteArray[0];

        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        coroutine = StartCoroutine(PlayOpenRedAnimUIHanlder());
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
        HideButton();
        leftUpgrade.Active();
        CloseBook();
    }

    public void ClickOnRight()
    {
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
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }

            coroutine = StartCoroutine(PlayCloseBlueAnimUIHanlder());
        }
        else
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }

            coroutine = StartCoroutine(PlayCloseRedAnimUIHanlder());
        }

        GameManager.Instance.Continuous();
        CursorManager.Instance.SetCursor1();
    }

    private IEnumerator PlayOpenBlueAnimUIHanlder()
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

    private IEnumerator PlayOpenRedAnimUIHanlder()
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

    private IEnumerator PlayCloseBlueAnimUIHanlder()
    {
        for (int i = blueSpriteArray.Length - 1; i >= 0; i--)
        {
            SetPivotBySprite(blueSpriteArray[i]);
            image.sprite = blueSpriteArray[i];
            yield return new WaitForSecondsRealtime(speedSlide);
        }

        image.sprite = null;
        gameObject.SetActive(false);
        yield return new WaitForSecondsRealtime(1.5f);
    }

    private IEnumerator PlayCloseRedAnimUIHanlder()
    {
        for (int i = redSpriteArray.Length - 1; i >= 0; i--)
        {
            SetPivotBySprite(redSpriteArray[i]);
            image.sprite = redSpriteArray[i];
            yield return new WaitForSecondsRealtime(speedSlide);
        }

        image.sprite = null;
        gameObject.SetActive(false);
        yield return new WaitForSecondsRealtime(1.5f);
    }

    private void SetPivotBySprite(Sprite _sprite)
    {
        rectTransform.pivot = new Vector2(_sprite.pivot.x / _sprite.rect.width, _sprite.pivot.y / _sprite.rect.height);
    }
}
