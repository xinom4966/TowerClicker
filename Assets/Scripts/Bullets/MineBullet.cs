using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MineBullet : Bullet
{
    [SerializeField] private float explosionDelay = 2;
    [SerializeField] private SpriteRenderer myRenderer;
    private List<Collider2D> colliders = new List<Collider2D>();
    private Color color = new Color(1, 0, 0);
    private void Start()
    {
        StartCoroutine(Explode());
    }

    private void Update()
    {
        myRenderer.color += color;
    }

    /*private void Explode()
    {
        ContactFilter2D contactFilter = new ContactFilter2D();
        int hitbox = Physics2D.OverlapCircle(transform.position, range, contactFilter, colliders);
        foreach (Collider2D collider in colliders)
        {
            if (collider.GetComponent<Enemy>())
            {
                collider.GetComponent<Enemy>().TakeDamage(damage);
            }
        }
        pool.Release(this);
    }*/

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(explosionDelay);
        ContactFilter2D contactFilter = new ContactFilter2D();
        int hitbox = Physics2D.OverlapCircle(transform.position, range, contactFilter, colliders);
        foreach (Collider2D collider in colliders)
        {
            if (collider.GetComponent<Enemy>())
            {
                collider.GetComponent<Enemy>().TakeDamage(damage);
            }
        }
        Destroy(towerOrigin.transform.parent.gameObject);
        Destroy(gameObject);
    }
}
