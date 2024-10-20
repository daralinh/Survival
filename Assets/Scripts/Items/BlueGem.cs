using UnityEngine;

public class BlueGem : AItem
{
    [SerializeField] private TrailRenderer trailRenderer;
    private BezierMovement bezierMovement = new BezierMovement();
    private float distanceFromPlayer;

    public override void Born(Vector2 _position)
    {
        base.Born(_position);
        trailRenderer.emitting = true;
    }

    protected override void FixedUpdate()
    {
        if (isMoving)
        {
            bezierMovement.MoveFollowCubicBezierPointFixedUpdate(transform, PlayerController.Instance.transform.position , false);

            if (bezierMovement.inTheEnd)
            {
                isMoving = false;
                gameObject.SetActive(false);
                PoolingItem.Instance.BackToPool(this);
                return;
            }
        }

        distanceFromPlayer = Vector2.Distance(PlayerController.Instance.transform.position, transform.position);

        if (distanceFromPlayer <= 0.2f || distanceFromPlayer > 13f)
        {
            BackToBool();
            return;
        }

        countDisplayTime += Time.fixedDeltaTime;
        if (countDisplayTime >= displayTime)
        {
            isMoving = false;
            gameObject.SetActive(false);
            PoolingItem.Instance.BackToPool(this);
        }
    }

    protected override void MoveToPlayer()
    {
        circleCollider2D.enabled = false;
        bezierMovement.Reset(transform.position, PlayerController.Instance.transform.position, speed);
        isMoving = true;
    }

    protected override void BackToBool()
    {
        MusicManager.Instance.PlaySFX(EMusic.GetGem);
        trailRenderer.emitting = false;
        trailRenderer.Clear();
        base.BackToBool();
    }
}
