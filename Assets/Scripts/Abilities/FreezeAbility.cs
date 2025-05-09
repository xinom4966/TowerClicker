using System.Collections.Generic;
using UnityEngine;

public class FreezeAbility : Ability
{
    [SerializeField] private float _range = 1000;
    [SerializeField] private float _duration = 2;
    private List<Collider2D> _colliders = new List<Collider2D>();
    private float _timerTS = 0.0f;
    private bool _activated = false;

    protected override void Update()
    {
        if (_activated)
        {
            _timerTS += Time.deltaTime;
            if (_timerTS >= _duration)
            {
                _activated = false;
                _timerTS = 0.0f;
                RestartTime();
            }
            return;
        }
        base.Update();
    }

    public override void Execute()
    {
        base.Execute();
        if (!_usable || _activated)
        {
            return;
        }
        ContactFilter2D _contactFilter = new ContactFilter2D();
        int _hitbox = Physics2D.OverlapCircle(Vector2.zero, _range, _contactFilter, _colliders);
        foreach (Collider2D collider in _colliders)
        {
            if (collider.GetComponent<Ennemy>())
            {
                collider.GetComponent<Ennemy>().SetSpeed(0.0f);
            }
        }
        _activated = true;
        _usable = false;
    }

    private void RestartTime()
    {
        ContactFilter2D _contactFilter = new ContactFilter2D();
        int _hitbox = Physics2D.OverlapCircle(Vector2.zero, _range, _contactFilter, _colliders);
        foreach (Collider2D collider in _colliders)
        {
            if (collider.GetComponent<Ennemy>())
            {
                collider.GetComponent<Ennemy>().ResetSpeed();
            }
        }
    }
}
