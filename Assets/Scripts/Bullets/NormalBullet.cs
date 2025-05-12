using UnityEngine;

public class NormalBullet : Bullet
{
    private void Update()
    {
        if (!target.isActiveAndEnabled)
        {
            pool.Release(this);
            return;
        }
        if (Vector2.Distance(transform.position, target.transform.position) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
        else
        {
            target.TakeDamage(damage);
            pool.Release(this);
        }
    }
}
