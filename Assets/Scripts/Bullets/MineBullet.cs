using System.Collections.Generic;
using UnityEngine;

public class MineBullet : Bullet
{
    private List<Collider2D> _colliders;
    private void Start()
    {
        Explode();
    }

    private void Explode()
    {
        ContactFilter2D _contactFilter = new ContactFilter2D();
        int _hitbox = Physics2D.OverlapCircle(Vector2.zero, _range, _contactFilter, _colliders);
        foreach (Collider2D collider in _colliders)
        {
            if (collider.GetComponent<Ennemy>())
            {
                collider.GetComponent<Ennemy>().TakeDamage(_damage);
            }
        }
        _pool.Release(this);
    }
}
