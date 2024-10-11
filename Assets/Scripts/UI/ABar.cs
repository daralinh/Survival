using UnityEngine;
using UnityEngine.UI;

public abstract class ABar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public Text text;

    protected virtual void Awake()
    {
        slider.value = 0;
    }

    public virtual void SetMaxValue(int _maxValue)
    {
        slider.maxValue = _maxValue;
        fill.color = gradient.Evaluate(0f);
        text.text = $"{slider.value}/{_maxValue}";
    }

    public virtual void SetCurrentValue(int _currentValue)
    {
        slider.value = _currentValue;
        fill.color = gradient.Evaluate(slider.normalizedValue);
        text.text = $"{_currentValue}/{slider.maxValue}";
    }
}
