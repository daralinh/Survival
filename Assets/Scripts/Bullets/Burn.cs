using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class Burn : ABullet
{
    [SerializeField] private float burnSpeed;
    private float countBurnTime;
    private GameObject target;

    private Animator animator;

    protected override void Born()
    {
        tag = ETag.Burn.ToString();
        animator = GetComponent<Animator>();
        capsuleCollider.enabled = false;
        rb2D.isKinematic = true;
        countBurnTime = 0;
        base.Born();
    }

    protected override void FixedUpdate()
    {
        if (isShooting)
        {
            countTimeToHide += Time.fixedDeltaTime;
            countBurnTime += Time.fixedDeltaTime;

            if (countBurnTime >= (1 / burnSpeed))
            {
                MusicManager.Instance.PlayBulletSFX(EMusic.FireSpot);
                countBurnTime = 0;
                AHpManager hpManager = target.GetComponent<AHpManager>();
                hpManager.TakeDMG(dmg + UpgradeManager.Instance.BuffDMG, transform.position);
            }

            if (countTimeToHide > timeToHide)
            {
                isShooting = false;
                BackToPool();
            }
        }
    }

    public override void StartShooting(Transform source, Vector2 targetPosition, ELayer _targetLayer)
    {
    }

    public void StartShooting(GameObject _target)
    {   
        target = _target;
        transform.SetParent(_target.transform);
        transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        isShooting = true;
        gameObject.SetActive(true);
        animator.SetTrigger(EAnimation.Shoot.ToString());
    }

    protected override void BackToPool()
    {
        StartCoroutine(Handler());
    }

    private IEnumerator Handler()
    {
        animator.SetTrigger(EAnimation.Death.ToString());
        yield return new WaitForSeconds(0.2f);

        gameObject.SetActive(false);
        countTimeToHide = 0;
        PoolingBullet.Instance.BackToPool(this);
    }

    protected override void OnTriggerEnter2D(Collider2D collision2D)
    {
    }
}
