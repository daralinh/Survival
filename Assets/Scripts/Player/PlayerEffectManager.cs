using System.Collections;
using UnityEngine;

public class PlayerEffectManager : AEffectManager
{
    public override void ActiveEffect(AEffect _aEffect)
    {
        switch (_aEffect)
        {
            case SlowEffect _slowEffect:
                ActiveSlowEffect(_slowEffect);
                break;
            case BindEffect _bindEffect:
                ActiveBindEffect(_bindEffect);
                break;
            default:
                break;
        }
    }

    // Slow
    private void ActiveSlowEffect(SlowEffect _slowEffect)
    {
        if (!isApplingSlowEffect)
        {
            isApplingSlowEffect = true;
            coroutineSlowEffect = StartCoroutine(CoroutineSlowEffect(_slowEffect));
        }
        else
        {
            StopCoroutine(coroutineSlowEffect);
            EndSlowEffect();
            isApplingSlowEffect = true;
            coroutineSlowEffect = StartCoroutine(CoroutineSlowEffect(_slowEffect));
        }
    }

    private IEnumerator CoroutineSlowEffect(SlowEffect _slowEffect)
    {
        Debug.Log("slow in " + _slowEffect.ActiveTime + "giay");
        PlayerController.Instance.SetCanChangeSpeed(false);
        PlayerController.Instance.ReduceSpeedByPercent(_slowEffect.ValueReduceSpeedPercent);
        yield return new WaitForSeconds(_slowEffect.ActiveTime);
        
        EndSlowEffect();
    }

    private void EndSlowEffect()
    {
        Debug.Log("End Slow");
        PlayerController.Instance.SetCanChangeSpeed(true);
        PlayerController.Instance.BackToOriginSpeed();
        isApplingSlowEffect = false;
    }

    // Bind
    private void ActiveBindEffect(BindEffect _bindEffect)
    {
        if (!isApplingBindEffect)
        {
            isApplingBindEffect = true;
            coroutineBindEffect = StartCoroutine(CoroutineBindEffect(_bindEffect));
        }
        else
        {
            StopCoroutine(coroutineBindEffect);
            EndBindEffect();
            isApplingBindEffect = true;
            coroutineBindEffect = StartCoroutine(CoroutineBindEffect(_bindEffect));
        }
    }

    private IEnumerator CoroutineBindEffect(BindEffect _bindEffect)
    {
        PlayerController.Instance.SetCanChangeSpeed(false);
        PlayerController.Instance.ReduceSpeedByPercent(100);
        yield return new WaitForSeconds(_bindEffect.ActiveTime);

        EndBindEffect();
    }

    private void EndBindEffect()
    {
        PlayerController.Instance.SetCanChangeSpeed(true);
        PlayerController.Instance.BackToOriginSpeed();
        isApplingBindEffect = false;
    }
}
