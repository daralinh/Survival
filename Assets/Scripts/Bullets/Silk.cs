using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class Silk : ABullet
{
    [SerializeField] private float bindTime;
    private Animator animator;
    public AEffect EffectOfBullet { get; protected set; }
    protected override void Awake()
    {
        base.Awake();
        EffectOfBullet = new BindEffect(bindTime);
        animator = GetComponent<Animator>();
    }

    public void StopMoveEvent()
    {
        speed = 0;
    }

    public override void StartShooting(Transform source, Vector2 targetPosition, ELayer _targetLayer)
    {
        speed = originSpeed;
        base.StartShooting(source, targetPosition, _targetLayer);
        animator.SetBool(EAnimation.Shoot.ToString(), true);
    }

    protected override void BackToPool()
    {
        base.BackToPool();
        animator.SetBool(EAnimation.Shoot.ToString(), false);
        animator.SetTrigger(EAnimation.Idle.ToString());
    }

    protected override void OnTriggerEnter2D(Collider2D collision2D)
    {
        if (collision2D.gameObject.layer == LayerMask.NameToLayer(targetLayer.ToString()))
        {
            AHpManager hpComponent = collision2D.gameObject.GetComponents<Component>().FirstOrDefault(c => c is AHpManager) as AHpManager;

            if (hpComponent != null)
            {
                hpComponent.TakeDMG(dmg, sourceTransform.position);
                hpComponent.TakeEffect(EffectOfBullet);
                StopMoveEvent();
            }
        }
    }
}
