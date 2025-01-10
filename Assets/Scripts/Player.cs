using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float _clickCooldown;
    [SerializeField] private LoadingCircle _loadingCircle;
    [SerializeField] private int _murderAward;
    [SerializeField] private TextMeshProUGUI _goldDisplay;
    private float _timer;
    private Vector2 _mousePos;
    private Camera _camera;
    private int _money;

    private void Start()
    {
        _camera = Camera.main;
        _timer = 0.0f;
        _money = 0;
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

    private void UpdateGoldDisplay()
    {
        _goldDisplay.text = "gold : " + _money;
    }

    public void OnEnnemyKilled()
    {
        _money += _murderAward;
        UpdateGoldDisplay();
    }

    public float GetCoolDown()
    {
        return _timer;
    }

    public bool CheckPrice(Tower tower)
    {
        return (_money - tower.GetCost() >= 0);
    }

    public void DoTransaction(int debt)
    {
        _money -= debt;
        UpdateGoldDisplay();
    }
}
