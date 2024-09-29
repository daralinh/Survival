public abstract class AEffect
{
    protected static int id = 0;

    public int Id { get; protected set; }
    public bool AppliedEffect { get; protected set; }
    public EEffectApplied EffectName { get; protected set; }

    public abstract void ActiveEffect();
}
