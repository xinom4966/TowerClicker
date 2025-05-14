using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnRate;
    [SerializeField] private List<Transform> wayPoints;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float enemySpeed;
    [SerializeField] private float enemyAcceleration;
    [SerializeField] private float spawnAcceleration;
    [SerializeField] private UnityEvent onKilledEvent;
    [SerializeField] private UnityEvent lossEvent;
    [SerializeField] private float enemySpeedCap;
    [SerializeField] private float spawnSpeedCap;
    [SerializeField] private int enemyHealth;
    private float timer;
    private Pool<Enemy> enemyPool;

    private void Start()
    {
        timer = 4.0f;
        enemyPool = new Pool<Enemy>(CreateEnemy, OnGetEnemy, OnReleaseEnemy, 10);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnRate)
        {
            timer = 0.0f;
            Enemy enemy = enemyPool.Get();
            enemy.transform.position = transform.position;
        }
    }

    private Enemy CreateEnemy()
    {
        GameObject enemyGO = Instantiate(enemyPrefab);
        Enemy enemy = enemyGO.GetComponent<Enemy>();
        enemy.SetHP(enemyHealth);
        enemy.SetWayPoints(wayPoints);
        enemy.SetSpeed(enemySpeed);
        enemy.onLoseEvent.AddListener(InvokeLossEvent);
        return enemy;
    }

    private void OnGetEnemy(Enemy enemy)
    {
        enemy.SetHP(enemyHealth);
        enemy.SetWayPoints(wayPoints);
        enemy.SetSpeed(enemySpeed);
        enemy.gameObject.SetActive(true);
    }

    private void OnReleaseEnemy(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
        onKilledEvent.Invoke();
    }

    public void SpeedUp()
    {
        if (enemySpeed * enemyAcceleration <= enemySpeedCap)
        {
            enemySpeed *= enemyAcceleration;
        }
        if (spawnRate * spawnAcceleration >= spawnSpeedCap)
        {
            spawnRate *= spawnAcceleration;
        }
        enemyHealth++;
        enemyPrefab.GetComponent<Enemy>().AddHP();
    }

    private void InvokeLossEvent()
    {
        lossEvent.Invoke();
    }
}
