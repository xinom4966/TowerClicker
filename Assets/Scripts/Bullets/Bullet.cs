using UnityEngine;

public class Bullet : MonoBehaviour, IpoolInterface<Bullet>
{
    protected Pool<Bullet> pool;
    protected Enemy target;
    protected Tower towerOrigin;
    protected float speed;
    protected int damage;
    protected float range;

    public void SetDatas(Enemy p_target, Tower p_towerOrigin, float p_speed, int p_damage, float p_range)
    {
        target = p_target;
        towerOrigin = p_towerOrigin;
        speed = p_speed;
        damage = p_damage;
        range = p_range;
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public void SetPool(Pool<Bullet> pool)
    {
        this.pool = pool;
    }
}
