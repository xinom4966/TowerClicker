using System.Collections.Generic;
using UnityEngine;

public class FreezeAbility : Ability
{
    [SerializeField] private float range = 1000;
    [SerializeField] private float duration = 2;
    private List<Collider2D> colliders = new List<Collider2D>();
    private float timerTS = 0.0f;
    private bool activated = false;
    private ContactFilter2D contactFilter = new ContactFilter2D();
    private int hitbox = 0;

    protected override void Update()
    {
        if (activated)
        {
            timerTS += Time.deltaTime;
            if (timerTS >= duration)
            {
                activated = false;
                timerTS = 0.0f;
                RestartTime();
            }
            return;
        }
        base.Update();
    }

    public override void Execute()
    {
        base.Execute();
        if (!usable || activated)
        {
            return;
        }
        hitbox = Physics2D.OverlapCircle(Vector2.zero, range, contactFilter, colliders);
        foreach (Collider2D collider in colliders)
        {
            if (collider.GetComponent<Enemy>())
            {
                collider.GetComponent<Enemy>().SetSpeed(0.0f);
            }
        }
        activated = true;
        usable = false;
    }

    private void RestartTime()
    {
        hitbox = Physics2D.OverlapCircle(Vector2.zero, range, contactFilter, colliders);
        foreach (Collider2D collider in colliders)
        {
            if (collider.GetComponent<Enemy>())
            {
                collider.GetComponent<Enemy>().ResetSpeed();
            }
        }
    }
}
