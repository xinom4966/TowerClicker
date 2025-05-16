using UnityEngine;

public class LaserBullet : Bullet
{
    [SerializeField] private SpriteRenderer laserRenderer;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float hitRate;
    private float distance;
    private Vector3 direction;
    private float timer;

    private void Update()
    {
        /*distance = Vector2.Distance(target.transform.position, towerOrigin.transform.position);
        direction = target.transform.position - towerOrigin.transform.position;
        transform.position = towerOrigin.transform.position + direction / 2;

        //Rotation du laser en fonction de l'ennemi en déplacement
        float angle = Mathf.Atan2(target.transform.position.y - transform.position.y, target.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        //Rescale en fonction de la distance entre la tour d'origine et l'ennemi visé
        transform.localScale = new(distance / towerOrigin.transform.localScale.x, 0.2f, 1);*/

        distance = Vector2.Distance(target.transform.position, towerOrigin.transform.position);
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, target.transform.position);

        //Fait des dégats toutes les n secondes (n étant _hitRate)
        timer += Time.deltaTime;
        if (timer >= hitRate)
        {
            timer = 0.0f;
            target.TakeDamage(damage);
        }

        //Despawn le laser si l'ennemi est hors de portée ou mort
        if (distance > range + 1 || !target.isActiveAndEnabled)
        {
            pool.Release(this);
        }
    }
}
