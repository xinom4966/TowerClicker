using System.Collections.Generic;
using UnityEngine;

public class Ennemy : MonoBehaviour, IpoolInterface<Ennemy>
{
    [SerializeField] private int _healthPoints;
    [SerializeField] private float _speed;
    public List<Transform> wayPoints;
    private int _positionIndex;
    private Pool<Ennemy> _pool;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (Vector2.Distance(transform.position, wayPoints[_positionIndex].position) < 0.02f)
        {
            _positionIndex++;
            if (_positionIndex == wayPoints.Count)
            {
                _positionIndex = 0;
                _pool.Release(this);
                return;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, wayPoints[_positionIndex].position, _speed * Time.deltaTime);
    }

    private void OnMouseDown()
    {
        TakeDamage(1);
    }

    public void TakeDamage(int damageAmmount)
    {
        _healthPoints -= damageAmmount;
        if ( _healthPoints < 0)
        {
            _pool.Release(this);
        }
    }

    public void SetHP(int ammount)
    {
        _healthPoints = ammount;
    }

    public void SetWayPoints(List<Transform> p_wayPoints)
    {
        wayPoints.Clear();
        wayPoints = p_wayPoints;
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
