using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectManager : AEffectManager
{
    public override void ActiveEffect(AEffect _aEffect)
    {
        switch (_aEffect)
        {
            case PlayerSlowEffect _slowEffect:
                ActiveSlowEffect(_slowEffect);
                break;
            default:
                break;
        }
    }

    private void ActiveSlowEffect(PlayerSlowEffect _slowEffect)
    {
        if (!isApplingSlowEffect)
        {
            coroutineSlowEffect = StartCoroutine(CoroutineSlowEffect(_slowEffect));
        }
    }

    private IEnumerator CoroutineSlowEffect(PlayerSlowEffect _slowEffect)
    {
        isApplingSlowEffect = true;
        PlayerController.Instance.SetCanChangeSpeed(false);
        PlayerController.Instance.ReduceSpeedByPercent(_slowEffect.ValueReduceSpeedPercent);
        yield return new WaitForSeconds(_slowEffect.ActiveTime);
        
        EndSlowEffect();
    }

    private void EndSlowEffect()
    {
        PlayerController.Instance.SetCanChangeSpeed(true);
        PlayerController.Instance.BackToOriginSpeed();
    }
}
