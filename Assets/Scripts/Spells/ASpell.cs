using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ASpell : MonoBehaviour
{
    [SerializeField] protected float attackRange;
    [SerializeField] protected float attackSpeed;
    [SerializeField] protected float numberShootInPerAttack;
    protected int countNumberShootInPerAttack;

    protected Coroutine coroutine;

    protected virtual void Awake()
    {
        coroutine = null;
        countNumberShootInPerAttack = 0;
        ActiveSpell();
    }

    public virtual void ActiveSpell()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        coroutine = StartCoroutine(CoroutineHandler());
    }

    protected virtual IEnumerator CoroutineHandler()
    {
        while (true)
        {
            Handler();
            yield return new WaitForSeconds(1 / attackSpeed);
        }
    }

    protected virtual void Handler()
    {

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
