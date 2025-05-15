using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private float fireRate;
    [SerializeField] private int damage;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int cost;
    [SerializeField] private CircleCollider2D towerCollider;
    [SerializeField] private float slowAmmount = 0;
    private GameObject instantiatedBullet;
    private Bullet bulletScript;
    private List<Enemy> targetList = new List<Enemy>();
    private Pool<Bullet> bulletPool;
    private float timer;
    private MunitionType munition;
    private bool hasFired;

    private void Start()
    {
        bulletPool = new Pool<Bullet>(CreateBullet, OnGetBullet, OnReleaseBullet, 10);
        timer = 0.0f;
        bulletScript = bulletPrefab.GetComponent<Bullet>();
        switch (bulletScript)
        {
            case NormalBullet:
                munition = MunitionType.NormalBullet;
                break;
            case LaserBullet:
                munition = MunitionType.Laser;
                break;
            case FreezeBullet:
                munition = MunitionType.FreezeBullet;
                break;
            case GrappleBullet:
                munition = MunitionType.GrappleBullet;
                break;
            case MineBullet:
                munition = MunitionType.MineBullet;
                break;
        }
        hasFired = false;
    }

    private void Update()
    {
        switch (munition)
        {
            case MunitionType.NormalBullet:
                timer += Time.deltaTime;
                if (timer >= fireRate)
                {
                    timer = 0.0f;
                    if (targetList.Count > 0)
                    {
                        Shoot(targetList[0]);
                    }
                }
                break;
            case MunitionType.Laser:
                if (!hasFired && targetList.Count > 0)
                {
                    Shoot(targetList[0]);
                    hasFired = true;
                }
                break;
            case MunitionType.FreezeBullet:
                FreezeEnnemies();
                break;
            case MunitionType.GrappleBullet:
                if (!hasFired && targetList.Count > 0)
                {
                    Shoot(targetList[0]);
                    hasFired = true;
                }
                break;
            case MunitionType.MineBullet:
                WaitForTrigger();
                break;
        }
    }

    private Bullet CreateBullet()
    {
        instantiatedBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bulletScript = instantiatedBullet.GetComponent<Bullet>();
        return bulletScript;
    }

    private void OnGetBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    private void OnReleaseBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
        bullet.transform.position = transform.position;
        hasFired = false;
    }

    private void Shoot(Enemy target)
    {
        Bullet bullet = bulletPool.Get();
        bullet.SetDatas(target, this, bulletSpeed, damage, towerCollider.radius);
    }

    private void FreezeEnnemies()
    {
        if (targetList.Count > 0)
        {
            foreach (Enemy target in targetList)
            {
                target.Slow(slowAmmount);
            }
        }
    }

    private void WaitForTrigger()
    {
        if (targetList.Count > 0)
        {
            Shoot(targetList[0]);
            Destroy(transform.parent.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.GetComponent<Enemy>())
        {
            return;
        }
        Enemy newTarget = collision.gameObject.GetComponent<Enemy>();
        targetList.Add(newTarget);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.GetComponent<Enemy>())
        {
            return;
        }
        Enemy targetToRelease = collision.gameObject.GetComponent<Enemy>();
        targetList.Remove(targetToRelease);
        if (munition == MunitionType.FreezeBullet)
        {
            targetToRelease.ResetSpeed();
        }
    }

    public int GetCost()
    {
        return cost;
    }
}

public enum MunitionType
{
    NormalBullet,
    Laser,
    FreezeBullet,
    GrappleBullet,
    MineBullet
}
