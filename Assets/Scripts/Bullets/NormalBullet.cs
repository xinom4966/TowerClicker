using UnityEngine;

public class NormalBullet : Bullet
{
    private void Update()
    {
        if (!_target.isActiveAndEnabled)
        {
            _pool.Release(this);
            return;
        }
        if (Vector2.Distance(transform.position, _target.transform.position) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, _target.transform.position, _speed * Time.deltaTime);
        }
        else
        {
            _target.TakeDamage(_damage);
            _pool.Release(this);
        }
    }
}
