using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AEffectManager : MonoBehaviour
{
    protected Coroutine coroutineSlowEffect;
    protected bool isApplingSlowEffect;

    protected void Awake()
    {
        coroutineSlowEffect = null;
        isApplingSlowEffect = false;
    }

    public abstract void ActiveEffect(AEffect aEffect);
}
