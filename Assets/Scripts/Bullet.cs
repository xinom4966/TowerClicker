using UnityEngine;

public class Bullet : MonoBehaviour, IpoolInterface<Bullet>
{
    private Ennemy _target;
    private float _speed;
    private int _damage;
    private Pool<Bullet> _pool;

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

    public void SetDatas(Ennemy p_target, float p_speed, int p_damage)
    {
        _target = p_target;
        _speed = p_speed;
        _damage = p_damage;
    }

    public void SetPool(Pool<Bullet> pool)
    {
        _pool = pool;
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}
