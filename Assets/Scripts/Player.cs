using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float _clickCooldown;
    [SerializeField] private ProgressCircle _loadingCircle;
    private float _timer;
    private Vector2 _mousePos;
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
        _timer = 0.0f;
    }

    private void Update()
    {
        _mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
        _timer -= Time.deltaTime;
    }

    public void OnClick(InputAction.CallbackContext ctx)
    {
        if (_timer >= 0)
        {
            return;
        }
        RaycastHit2D hit = Physics2D.Raycast(_mousePos, _camera.transform.forward);
        if (hit.collider == null)
        {
            return;
        }
        if (hit.collider.GetComponent<Ennemy>() == null)
        {
            return;
        }
        Ennemy hitEnnemy = hit.collider.GetComponent<Ennemy>();
        _timer = _clickCooldown;
        _loadingCircle.SetParent(this);
        _loadingCircle.SetMaxTimer(_clickCooldown);
        _loadingCircle.gameObject.SetActive(true);
        hitEnnemy.TakeDamage(1);
    }

    public float GetCoolDown()
    {
        return _timer;
    }
}
