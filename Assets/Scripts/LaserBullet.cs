using UnityEngine;

public class LaserBullet : Bullet
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private float _hitRate;
    private float distance;
    private Vector3 direction;
    private float _timer;

    private void Update()
    {
        distance = Vector2.Distance(_target.transform.position, _towerOrigin.transform.position);
        direction = _target.transform.position - _towerOrigin.transform.position;
        transform.position = _towerOrigin.transform.position + direction / 2;

        //Rotation du laser en fonction de l'ennemi en déplacement
        float angle = Mathf.Atan2(_target.transform.position.y - transform.position.y, _target.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        //Rescale en fonction de la distance entre la tour d'origine et l'ennemi visé
        transform.localScale = new(distance / _towerOrigin.transform.localScale.x, 0.2f, 1);

        //Fait des dégats toutes les n secondes (n étant _hitRate)
        _timer += Time.deltaTime;
        if (_timer >= _hitRate)
        {
            _timer = 0.0f;
            _target.TakeDamage(_damage);
        }

        //Despawn le laser si l'ennemi est hors de portée ou mort
        if (distance > _range || !_target.isActiveAndEnabled)
        {
            _pool.Release(this);
        }
    }
}
