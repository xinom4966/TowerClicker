using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private float _fireRate;
    [SerializeField] private int _damage;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private int _cost;
    [SerializeField] private CircleCollider2D _collider;
    [SerializeField] private float _slowAmmount = 0;
    private GameObject _instantiatedBullet;
    private Bullet _bulletScript;
    private List<Ennemy> _targetList = new List<Ennemy>();
    private Pool<Bullet> _bulletPool;
    private float _timer;
    private MunitionType _munition;
    private bool _hasFired;

    private void Start()
    {
        _bulletPool = new Pool<Bullet>(CreateBullet, OnGetBullet, OnReleaseBullet, 10);
        _timer = 0.0f;
        _bulletScript = _bulletPrefab.GetComponent<Bullet>();
        switch (_bulletScript)
        {
            case NormalBullet:
                _munition = MunitionType.NormalBullet;
                break;
            case LaserBullet:
                _munition = MunitionType.Laser;
                break;
            case FreezeBullet:
                _munition = MunitionType.FreezeBullet;
                break;
            case GrappleBullet:
                _munition = MunitionType.GrappleBullet;
                break;
        }
        _hasFired = false;
    }

    private void Update()
    {
        switch (_munition)
        {
            case MunitionType.NormalBullet:
                _timer += Time.deltaTime;
                if (_timer >= _fireRate)
                {
                    _timer = 0.0f;
                    if (_targetList.Count > 0)
                    {
                        Shoot(_targetList[0]);
                    }
                }
                break;
            case MunitionType.Laser:
                if (!_hasFired && _targetList.Count > 0)
                {
                    Shoot(_targetList[0]);
                    _hasFired = true;
                }
                break;
            case MunitionType.FreezeBullet:
                FreezeEnnemies();
                break;
            case MunitionType.GrappleBullet:
                if (!_hasFired && _targetList.Count > 0)
                {
                    Shoot(_targetList[0]);
                    _hasFired = true;
                }
                break;
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
        bullet.gameObject.SetActive(true);
    }

    private void OnReleaseBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
        bullet.transform.position = transform.position;
        _hasFired = false;
    }

    private void Shoot(Ennemy target)
    {
        Bullet bullet = _bulletPool.Get();
        bullet.SetDatas(target, this, _bulletSpeed, _damage, _collider.radius);
    }

    private void FreezeEnnemies()
    {
        if (_targetList.Count > 0)
        {
            foreach (Ennemy target in _targetList)
            {
                target.Slow(_slowAmmount);
            }
        }
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
        if (_munition == MunitionType.FreezeBullet)
        {
            targetToRelease.ResetSpeed();
        }
    }

    public int GetCost()
    {
        return _cost;
    }
}

public enum MunitionType
{
    NormalBullet,
    Laser,
    FreezeBullet,
    GrappleBullet
}
