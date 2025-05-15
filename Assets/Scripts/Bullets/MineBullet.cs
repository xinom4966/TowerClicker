using System.Collections.Generic;
using UnityEngine;

public class MineBullet : Bullet
{
    private List<Collider2D> colliders = new List<Collider2D>();
    private void Start()
    {
        Explode();
    }

    private void Explode()
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
    }
}
