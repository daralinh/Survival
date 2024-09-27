using UnityEngine;

public class PlayerHpManager : AHpManager
{
    public override void TakeDMG(float dmg, Vector2 sourceDMG, EEffectApplied effectApplied)
    {
        currentHp = Mathf.Min(0, currentHp - dmg);
        PlayerController.Instance.TakeDMGHandler(sourceDMG, effectApplied);
    }
}
