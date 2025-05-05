using UnityEngine;

public class Bullet : MonoBehaviour, IpoolInterface<Bullet>
{
    protected Pool<Bullet> _pool;
    protected Ennemy _target;
    protected Tower _towerOrigin;
    protected float _speed;
    protected int _damage;
    protected float _range;

    public void SetDatas(Ennemy p_target, Tower p_towerOrigin, float p_speed, int p_damage, float p_range)
    {
        _target = p_target;
        _towerOrigin = p_towerOrigin;
        _speed = p_speed;
        _damage = p_damage;
        _range = p_range;
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public void SetPool(Pool<Bullet> pool)
    {
        _pool = pool;
    }
}
