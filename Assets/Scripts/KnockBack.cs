using System.Collections;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    [SerializeField] private float knockBackTime;
    private Rigidbody2D rb2D;
    private bool canKnockBack;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        canKnockBack = true;
    }

    public void GetKnockBack(Vector2 source, float force)
    {
        if (canKnockBack)
        {
            canKnockBack = false;
            Vector2 difference = ((Vector2)transform.position - source).normalized * force * rb2D.mass;
            rb2D.AddForce(difference, ForceMode2D.Impulse);
            StartCoroutine(Handler());
        }
    }

    private IEnumerator Handler()
    {
        yield return new WaitForSeconds(knockBackTime);
        rb2D.velocity = Vector2.zero;
        canKnockBack = true;
    }
}
