using UnityEngine;

public class BlueGem : AItem
{
    [SerializeField] private TrailRenderer trailRenderer;

    public override void Born(Vector2 _position)
    {
        base.Born(_position);
        trailRenderer.emitting = true;
    }

    protected override void BackToBool()
    {
        MusicManager.Instance.PlaySFX(EMusic.GetGem);
        trailRenderer.emitting = false;
        trailRenderer.Clear();
        base.BackToBool();
    }
}
