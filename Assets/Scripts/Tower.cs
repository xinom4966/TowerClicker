using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private float _fireRate;
    [SerializeField] private int _damage;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private GameObject _bulletPrefab;
    private GameObject _instantiatedBullet;
    private Bullet _bulletScript;
    private List<Ennemy> _targetList = new List<Ennemy>();
    private Pool<Bullet> _bulletPool;
    private float _timer;

    private void Start()
    {
        _bulletPool = new Pool<Bullet>(CreateBullet, OnGetBullet, OnReleaseBullet, 10);
        _timer = 0.0f;
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _fireRate)
        {
            _timer = 0.0f;
            if (_targetList.Count > 0)
            {
                Shoot(_targetList[0]);
            }
        }
    }

    private Bullet CreateBullet()
    {
        _instantiatedBullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
        _bulletScript = _instantiatedBullet.GetComponent<Bullet>();
        return _bulletScript;
    }

    private void OnGetBullet(Bullet bullet)
    {
        bullet.gameObject.transform.position = transform.position;
        bullet.gameObject.SetActive(true);
    }

    private void OnReleaseBullet(Bullet bullet)
    {
        bullet.gameObject.transform.position = transform.position;
        bullet.gameObject.SetActive(false);
    }

    private void Shoot(Ennemy target)
    {
        Bullet bullet = _bulletPool.Get();
        bullet.SetDatas(target, _bulletSpeed, _damage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.GetComponent<Ennemy>())
        {
            return;
        }
        Ennemy newTarget = collision.gameObject.GetComponent<Ennemy>();
        _targetList.Add(newTarget);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.GetComponent<Ennemy>())
        {
            return;
        }
        Ennemy targetToRelease = collision.gameObject.GetComponent<Ennemy>();
        _targetList.Remove(targetToRelease);
    }
}
