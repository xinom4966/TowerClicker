using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnnemySpawner : MonoBehaviour
{
    [SerializeField] private float _spawnRate;
    [SerializeField] private List<Transform> _wayPoints;
    [SerializeField] private GameObject _ennemyPrefab;
    [SerializeField] private float _ennemySpeed;
    [SerializeField] private float _ennemyAcceleration;
    [SerializeField] private float _spawnAcceleration;
    [SerializeField] private UnityEvent _onKilledEvent;
    [SerializeField] private UnityEvent _lossEvent;
    [SerializeField] private float _ennemySpeedCap;
    [SerializeField] private float _spawnSpeedCap;
    [SerializeField] private int _EnnemyHealth;
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
        ennemy._onLoseEvent.AddListener(InvokeLossEvent);
        return ennemy;
    }

    private void OnGetEnnemy(Ennemy ennemy)
    {
        ennemy.SetHP(_EnnemyHealth);
        ennemy.SetWayPoints(_wayPoints);
        ennemy.SetSpeed(_ennemySpeed);
        ennemy.gameObject.SetActive(true);
    }

    private void OnReleaseEnnemy(Ennemy ennemy)
    {
        ennemy.gameObject.SetActive(false);
        _onKilledEvent.Invoke();
    }

    public void SpeedUp()
    {
        if (_ennemySpeed * _ennemyAcceleration <= _ennemySpeedCap)
        {
            _ennemySpeed *= _ennemyAcceleration;
        }
        if (_spawnRate * _spawnAcceleration >= _spawnSpeedCap)
        {
            _spawnRate *= _spawnAcceleration;
        }
        _EnnemyHealth++;
        _ennemyPrefab.GetComponent<Ennemy>().AddHP();
    }

    private void InvokeLossEvent()
    {
        _lossEvent.Invoke();
    }
}
