using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Ennemy : MonoBehaviour, IpoolInterface<Ennemy>
{
    [SerializeField] private int _BasehealthPoints;
    [SerializeField] private int _maxHealthPoints;
    [SerializeField] private float _clickCooldown;
    private int _healthPoints;
    private float _speed;
    private List<Transform> _wayPoints;
    private int _positionIndex;
    private Pool<Ennemy> _pool;
    private float _nextClick = 0.0f;

    private void Start()
    {
        _healthPoints = _BasehealthPoints;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (Vector2.Distance(transform.position, _wayPoints[_positionIndex].position) < 0.02f)
        {
            _positionIndex++;
            if (_positionIndex == _wayPoints.Count)
            {
                _positionIndex = 0;
                SceneManager.LoadScene("LoseScene");
                _pool.Release(this);
                return;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, _wayPoints[_positionIndex].position, _speed * Time.deltaTime);
    }

    private void OnMouseDown()
    {
        if (Time.time >= _nextClick)
        {
            TakeDamage(1);
            _nextClick = Time.time + _clickCooldown;
        }
    }

    public void TakeDamage(int damageAmmount)
    {
        _healthPoints -= damageAmmount;
        if ( _healthPoints <= 0)
        {
            _pool.Release(this);
        }
    }

    public void SetHP(int ammount)
    {
        _healthPoints = ammount;
    }

    public void AddHP()
    {
        if (_healthPoints >= _maxHealthPoints)
        {
            return;
        }
        _healthPoints++;
    }

    public void SetSpeed(float newSpeed)
    {
        _speed = newSpeed;
    }

    public void SetWayPoints(List<Transform> p_wayPoints)
    {
        _positionIndex = 0;
        _wayPoints = p_wayPoints;
    }

    public void SetPool(Pool<Ennemy> pool)
    {
        _pool = pool;
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}
