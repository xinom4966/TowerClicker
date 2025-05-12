using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float clickCooldown;
    [SerializeField] private LoadingCircle loadingCircle;
    [SerializeField] private int murderAward;
    [SerializeField] private TextMeshProUGUI goldDisplay;
    private float timer;
    private Vector2 mousePos;
    private Camera playerCamera;
    private int money;

    private void Start()
    {
        playerCamera = Camera.main;
        timer = 0.0f;
        money = 0;
    }

    private void Update()
    {
        mousePos = playerCamera.ScreenToWorldPoint(Input.mousePosition);
        timer -= Time.deltaTime;
    }

    public void OnClick(InputAction.CallbackContext ctx)
    {
        if (timer >= 0)
        {
            return;
        }
        RaycastHit2D hit = Physics2D.Raycast(mousePos, playerCamera.transform.forward);
        if (hit.collider == null)
        {
            return;
        }
        if (hit.collider.GetComponent<Enemy>() == null)
        {
            return;
        }
        Enemy hitEnemy = hit.collider.GetComponent<Enemy>();
        timer = clickCooldown;
        loadingCircle.SetParent(this);
        loadingCircle.SetMaxTimer(clickCooldown);
        loadingCircle.gameObject.SetActive(true);
        hitEnemy.TakeDamage(1);
    }

    private void UpdateGoldDisplay()
    {
        goldDisplay.text = "gold : " + money;
    }

    public void OnEnnemyKilled()
    {
        money += murderAward;
        UpdateGoldDisplay();
    }

    public float GetCoolDown()
    {
        return timer;
    }

    public bool CheckPrice(Tower tower)
    {
        return (money - tower.GetCost() >= 0);
    }

    public void DoTransaction(int debt)
    {
        money -= debt;
        UpdateGoldDisplay();
    }

    public int GetMurderAward()
    {
        return murderAward;
    }

    public void SetMurderAward(int newValue)
    {
        murderAward = newValue;
    }
}
