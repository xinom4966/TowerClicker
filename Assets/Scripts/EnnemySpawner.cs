using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemySpawner : MonoBehaviour
{
    [SerializeField] private float _spawnRate;
    [SerializeField] private List<Transform> _wayPoints;
    [SerializeField] private GameObject _ennemyPrefab;
    [SerializeField] private float _ennemySpeed;
    [SerializeField] private float _ennemyAcceleration;
    [SerializeField] private float _spawnAcceleration;
    private float _timer;
    private Pool<Ennemy> _ennemyPool;

    private void Start()
    {
        _timer = 4.0f;
        _ennemyPool = new Pool<Ennemy>(CreateEnnemy, OnGetEnnemy, OnReleaseEnnemy, 10);
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _spawnRate)
        {
            _timer = 0.0f;
            Ennemy ennemy = _ennemyPool.Get();
            ennemy.transform.position = transform.position;
        }
    }

    private Ennemy CreateEnnemy()
    {
        GameObject ennemyGO = Instantiate(_ennemyPrefab);
        Ennemy ennemy = ennemyGO.GetComponent<Ennemy>();
        ennemy.SetWayPoints(_wayPoints);
        ennemy.SetSpeed(_ennemySpeed);
        return ennemy;
    }

    private void OnGetEnnemy(Ennemy ennemy)
    {
        ennemy.SetHP(4);
        ennemy.SetWayPoints(_wayPoints);
        ennemy.SetSpeed(_ennemySpeed);
        ennemy.gameObject.SetActive(true);
    }

    private void OnReleaseEnnemy(Ennemy ennemy)
    {
        ennemy.gameObject.SetActive(false);
    }

    public void SpeedUp()
    {
        _ennemySpeed *= _ennemyAcceleration;
        _spawnRate *= _spawnAcceleration;
    }
}
