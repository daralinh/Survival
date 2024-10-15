using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PoolingTMP : Singleton<PoolingTMP>
{
    [SerializeField] private TMP tmpPrefab;
    [SerializeField] public Canvas canvas;
    
    private Queue<TMP> tmpQueue = new Queue<TMP>();
    private List<Color> tmpColors = new List<Color>();

    protected override void Awake()
    {
        base.Awake();
        Color _color;
        // white
        ColorUtility.TryParseHtmlString("#FFFFFF", out _color);
        tmpColors.Add(_color);
        // red 
        ColorUtility.TryParseHtmlString("#EF0F1D", out _color);
        tmpColors.Add(_color);
        // blue
        ColorUtility.TryParseHtmlString("#39B5E8", out _color);
        tmpColors.Add(_color);
        // purple
        ColorUtility.TryParseHtmlString("#6A50BF", out _color);
        tmpColors.Add(_color);
    }

    public void SpawnTMP(string _content, Vector2 _position, EColor _color)
    {
        if (tmpQueue.Count == 0)
        {
            tmpQueue.Enqueue(Instantiate(tmpPrefab, canvas.transform));
        }

        TMP _tmp = tmpQueue.Dequeue();
        _tmp.Born(_content, _position, GetColor(_color));
    }

    public void BackToPool(TMP _oldTMP)
    {
        if (tmpQueue.Contains(_oldTMP))
        {
            return;
        }

        tmpQueue.Enqueue(_oldTMP);
    }

    private Color GetColor(EColor _color)
    {
        switch (_color)
        {
            case EColor.Red:
                return tmpColors[1];

            case EColor.Blue:
                return tmpColors[2];
            case EColor.Purple:
                return tmpColors[3];

            default:
                return tmpColors[0];
        }
    }
}
