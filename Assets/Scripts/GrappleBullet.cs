using UnityEngine;

public class GrappleBullet : Bullet
{
    [SerializeField] private float _hitRate;
    private float distance;
    private Vector3 direction;
    private float _timer;
    private GrappleState _state;

    private void Start()
    {
        _state = GrappleState.Catching;
        _timer = 0.0f;
    }

    private void OnDisable()
    {
        _state = GrappleState.Catching;
        transform.localScale = Vector3.one;
        _timer = 0.0f;
    }

    private void OnEnable()
    {
        _state = GrappleState.Catching;
        transform.localScale = Vector3.one;
        _timer = 0.0f;
    }

    private void Update()
    {
        distance = Vector2.Distance(_target.transform.position, _towerOrigin.transform.position);
        direction = _target.transform.position - _towerOrigin.transform.position;

        if (_target == null || !_target.isActiveAndEnabled || distance > _range + 1)
        {
            _pool.Release(this);
        }

        if (_state == GrappleState.Catching)
        {
            transform.localScale = new(Vector2.Lerp(_towerOrigin.transform.position, _target.transform.position, _timer).x, 0.2f, 1);
            transform.position = _towerOrigin.transform.position + direction * _timer;
            _timer += Time.deltaTime;
            if (Vector2.Distance(transform.position + direction/2, _target.transform.position) < 0.1f)
            {
                _state = GrappleState.Fetching;
            }
        }

        if (_state == GrappleState.Fetching)
        {
            transform.localScale = new (Vector2.Lerp(_towerOrigin.transform.position, _target.transform.position, _timer).x, 0.2f, 1);
            transform.position = _towerOrigin.transform.position + direction * _timer;
            _target.transform.position = transform.position + direction / 2;
            _timer -= Time.deltaTime;
            if (Vector2.Distance(_towerOrigin.transform.position, transform.position) < 0.2f)
            {
                _state = GrappleState.Killing;
                transform.localScale = Vector3.one;
            }
        }

        if (_state == GrappleState.Killing)
        {
            _target.transform.position = _towerOrigin.transform.position;
            _timer += Time.deltaTime;
            if (_timer >= _hitRate)
            {
                _timer = 0.0f;
                _target.TakeDamage(_damage);
            }
        }

        //Rotation du grappin en fonction de l'ennemi en d�placement
        float angle = Mathf.Atan2(_target.transform.position.y - transform.position.y, _target.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    enum GrappleState
    {
        Catching,
        Fetching,
        Killing
    }
}
