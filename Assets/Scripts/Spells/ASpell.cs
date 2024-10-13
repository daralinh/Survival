using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ASpell : MonoBehaviour
{
    [SerializeField] protected float attackRange;
    [SerializeField] protected float coolDown;
    [SerializeField] protected float numberShootInPerAttack;

    protected int countNumberShootInPerAttack = 0;

    public float CoolDown => Mathf.Max(1.5f, coolDown - UpgradeManager.Instance.DecCoolDownSpell);

    protected virtual void Awake()
    {
        gameObject.SetActive(false);
    }

    public virtual void ActiveSpell()
    {
        gameObject.SetActive(true);

        StartCoroutine(CoroutineHandler());
    }

    protected virtual IEnumerator CoroutineHandler()
    {
        while (true)
        {
            //Debug.Log(name + " " + Time.time.ToString());
            Handler();
            yield return new WaitForSeconds(CoolDown);
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
