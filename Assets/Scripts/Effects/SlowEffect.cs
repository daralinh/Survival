public class SlowEffect : AEffect
{
    public float ValueReduceSpeedPercent { get; private set; }

    public SlowEffect(float _reduceSpeedPercent, float _activeTime)
    {
        EffectName = EEffectApplied.Slow;
        ValueReduceSpeedPercent = _reduceSpeedPercent;
        ActiveTime = _activeTime;
    }
}
