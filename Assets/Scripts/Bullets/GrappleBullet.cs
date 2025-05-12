using UnityEngine;

public class GrappleBullet : Bullet
{
    [SerializeField] private float hitRate;
    private float distance;
    private Vector3 direction;
    private float timer;
    private GrappleState state;

    private void Start()
    {
        state = GrappleState.Catching;
        timer = 0.0f;
    }

    private void OnDisable()
    {
        state = GrappleState.Catching;
        transform.localScale = Vector3.one;
        timer = 0.0f;
    }

    private void OnEnable()
    {
        state = GrappleState.Catching;
        transform.localScale = Vector3.one;
        timer = 0.0f;
    }

    private void Update()
    {
        distance = Vector2.Distance(target.transform.position, towerOrigin.transform.position);
        direction = target.transform.position - towerOrigin.transform.position;

        if (target == null || !target.isActiveAndEnabled || distance > range + 1)
        {
            pool.Release(this);
        }

        if (state == GrappleState.Catching)
        {
            if (target.CheckIsGrappled())
            {
                pool.Release(this);
            }
            transform.localScale = new(Vector2.Lerp(towerOrigin.transform.position, target.transform.position, timer).x, 0.2f, 1);
            transform.position = towerOrigin.transform.position + direction * timer;
            timer += Time.deltaTime;
            if (Vector2.Distance(transform.position + direction/2, target.transform.position) < 0.1f)
            {
                state = GrappleState.Fetching;
            }
        }

        if (state == GrappleState.Fetching)
        {
            transform.localScale = new (Vector2.Lerp(towerOrigin.transform.position, target.transform.position, timer).x, 0.2f, 1);
            transform.position = towerOrigin.transform.position + direction * timer;
            target.transform.position = transform.position + direction / 2;
            target.Grapple();
            timer -= Time.deltaTime;
            if (Vector2.Distance(towerOrigin.transform.position, transform.position) < 0.2f)
            {
                state = GrappleState.Killing;
                transform.localScale = Vector3.one;
            }
        }

        if (state == GrappleState.Killing)
        {
            target.transform.position = towerOrigin.transform.position;
            timer += Time.deltaTime;
            if (timer >= hitRate)
            {
                timer = 0.0f;
                target.TakeDamage(damage);
            }
        }

        //Rotation du grappin en fonction de l'ennemi en déplacement
        float angle = Mathf.Atan2(target.transform.position.y - transform.position.y, target.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    enum GrappleState
    {
        Catching,
        Fetching,
        Killing
    }
}
