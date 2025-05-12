using System.Collections.Generic;
using UnityEngine;

public class WipeAbility : Ability
{
    [SerializeField] private float range = 1000;
    private List<Collider2D> colliders = new List<Collider2D>();
    protected override void Update()
    {
        base.Update();
    }

    public override void Execute()
    {
        base.Execute();
        if (!usable)
        {
            return;
        }
        ContactFilter2D contactFilter = new ContactFilter2D();
        int hitbox = Physics2D.OverlapCircle(Vector2.zero, range, contactFilter,colliders);
        foreach(Collider2D collider in colliders)
        {
            if (collider.GetComponent<Enemy>())
            {
                collider.GetComponent<Enemy>().TakeDamage(int.MaxValue);
            }
        }
        usable = false;
    }
}
