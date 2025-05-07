using System.Collections.Generic;
using UnityEngine;

public class WipeAbility : Ability
{
    [SerializeField] private float _range = 100;
    private List<Collider2D> _colliders = new List<Collider2D>();
    protected override void Update()
    {
        base.Update();
    }

    public override void Execute()
    {
        base.Execute();
        if (!_usable)
        {
            return;
        }
        ContactFilter2D _contactFilter = new ContactFilter2D();
        int _hitbox = Physics2D.OverlapCircle(Vector2.zero, _range, _contactFilter,_colliders);
        foreach(Collider2D collider in _colliders)
        {
            if (collider.GetComponent<Ennemy>())
            {
                collider.GetComponent<Ennemy>().TakeDamage(int.MaxValue);
            }
        }
        _usable = false;
    }
}
