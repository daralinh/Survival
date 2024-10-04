using UnityEngine;

public abstract class AEffectManager : MonoBehaviour
{
    protected Coroutine coroutineSlowEffect;
    protected bool isApplingSlowEffect;

    protected Coroutine coroutineBindEffect;
    protected bool isApplingBindEffect;

    protected void Awake()
    {
        coroutineSlowEffect = null;
        isApplingSlowEffect = false;

        coroutineBindEffect = null;
        isApplingBindEffect = false;
    }

    public abstract void ActiveEffect(AEffect aEffect);
}
