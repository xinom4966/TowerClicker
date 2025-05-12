using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IpoolInterface<Enemy>
{
    [SerializeField] private int baseHealthPoints;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Color damageColor;
    [SerializeField] private Color frozenColor;
    [SerializeField] private GameObject goldVisualPrefab;
    [SerializeField] private Image healthbar;
    [SerializeField] private Gradient hpGradient;
    public UnityEvent onLoseEvent;
    private GameObject goldFeedBack;
    private Color baseColor;
    private int healthPoints;
    private float speed;
    private float baseSpeed;
    private List<Transform> wayPoints;
    private int positionIndex;
    private Pool<Enemy> pool;
    private bool isSlowed = false;
    private bool isGrappled = false;
    private float ratio = 0f;

    private void Start()
    {
        healthPoints = baseHealthPoints;
        baseColor = spriteRenderer.color;
        ratio = (float)healthPoints / (float)baseHealthPoints;
        healthbar.fillAmount = ratio;
        healthbar.color = hpGradient.Evaluate(ratio);
    }

    private void OnEnable()
    {
        baseSpeed = speed;
        isGrappled = false;
        ratio = (float)healthPoints / (float)baseHealthPoints;
        healthbar.fillAmount = ratio;
        healthbar.color = hpGradient.Evaluate(ratio);
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (Vector2.Distance(transform.position, wayPoints[positionIndex].position) < 0.02f)
        {
            positionIndex++;
            if (positionIndex == wayPoints.Count)
            {
                positionIndex = 0;
                onLoseEvent.Invoke();
                SceneManager.LoadScene("LoseScene");
                pool.Release(this);
                return;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, wayPoints[positionIndex].position, speed * Time.deltaTime);
    }

    public void TakeDamage(int damageAmmount)
    {
        healthPoints -= damageAmmount;
        ratio = (float)healthPoints / (float)baseHealthPoints;
        healthbar.fillAmount = ratio;
        healthbar.color = hpGradient.Evaluate(ratio);
        if ( healthPoints <= 0)
        {
            goldFeedBack = Instantiate(goldVisualPrefab);
            goldFeedBack.GetComponentInChildren<GoldFeedBack>().SetDatas(Camera.main.WorldToScreenPoint(transform.position), 5);
            StopAllCoroutines();
            spriteRenderer.color = baseColor;
            pool.Release(this);
            return;
        }
        StartCoroutine(DamageFeedBack());
    }

    IEnumerator DamageFeedBack()
    {
        spriteRenderer.color = damageColor;
        yield return new WaitForSeconds(0.15f);
        if (isSlowed)
        {
            spriteRenderer.color = frozenColor;
        }
        else
        {
            spriteRenderer.color = baseColor;
        }
    }

    public void SetHP(int ammount)
    {
        healthPoints = ammount;
        baseHealthPoints = ammount;
    }

    public void AddHP()
    {
        healthPoints++;
        baseHealthPoints++;
    }

    public void SetSpeed(float newSpeed)
    {
        baseSpeed = speed;
        speed = newSpeed;
        isSlowed = false;
    }

    public void Slow(float slowAmmount)
    {
        if (isSlowed)
        {
            return;
        }
        baseSpeed = speed;
        speed *= slowAmmount;
        isSlowed = true;
        spriteRenderer.color = frozenColor;
    }

    public void ResetSpeed()
    {
        speed = baseSpeed;
        isSlowed = false;
        spriteRenderer.color = baseColor;
    }

    public void SetWayPoints(List<Transform> p_wayPoints)
    {
        positionIndex = 0;
        wayPoints = p_wayPoints;
    }

    public void SetPool(Pool<Enemy> pool)
    {
        this.pool = pool;
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public void Grapple()
    {
        isGrappled = true;
    }

    public bool CheckIsGrappled()
    {
        return isGrappled;
    }
}
