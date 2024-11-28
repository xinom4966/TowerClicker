using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Ennemy : MonoBehaviour, IpoolInterface<Ennemy>
{
    [SerializeField] private int _BasehealthPoints;
    //[SerializeField] private int _maxHealthPoints;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Color _damageColor;
    [SerializeField] private Color _frozenColor;
    [SerializeField] private GameObject _goldVisualPrefab;
    private GameObject _goldFeedBack;
    private Color _baseColor;
    private int _healthPoints;
    private float _speed;
    private float _baseSpeed;
    private List<Transform> _wayPoints;
    private int _positionIndex;
    private Pool<Ennemy> _pool;
    private bool _isSlowed = false;

    private void Start()
    {
        _healthPoints = _BasehealthPoints;
        _baseColor = _spriteRenderer.color;
    }

    private void OnEnable()
    {
        _baseSpeed = _speed;
        //_spriteRenderer.color = _baseColor;
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

    public void TakeDamage(int damageAmmount)
    {
        _healthPoints -= damageAmmount;
        if ( _healthPoints <= 0)
        {
            _goldFeedBack = Instantiate(_goldVisualPrefab);
            _goldFeedBack.GetComponentInChildren<GoldFeedBack>().SetParentPosition(Camera.main.WorldToScreenPoint(transform.position));
            StopAllCoroutines();
            _spriteRenderer.color = _baseColor;
            _pool.Release(this);
            return;
        }
        StartCoroutine(DamageFeedBack());
    }

    IEnumerator DamageFeedBack()
    {
        _spriteRenderer.color = _damageColor;
        yield return new WaitForSeconds(0.15f);
        if (_isSlowed)
        {
            _spriteRenderer.color = _frozenColor;
        }
        else
        {
            _spriteRenderer.color = _baseColor;
        }
    }

    public void SetHP(int ammount)
    {
        /*if (ammount > _maxHealthPoints)
        {
            _healthPoints = _maxHealthPoints;
            return;
        }*/
        _healthPoints = ammount;
    }

    public void AddHP()
    {
        /*if (_healthPoints >= _maxHealthPoints)
        {
            return;
        }*/
        _healthPoints++;
    }

    public void SetSpeed(float newSpeed)
    {
        _speed = newSpeed;
        _isSlowed = false;
    }

    public void Slow(float slowAmmount)
    {
        if (_isSlowed)
        {
            return;
        }
        _baseSpeed = _speed;
        _speed *= slowAmmount;
        _isSlowed = true;
        _spriteRenderer.color = _frozenColor;
    }

    public void ResetSpeed()
    {
        _speed = _baseSpeed;
        _isSlowed = false;
        _spriteRenderer.color = _baseColor;
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
