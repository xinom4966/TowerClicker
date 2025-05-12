using UnityEngine;
using UnityEngine.UI;

public class LoadingCircle : MonoBehaviour
{
    private float maxTimer = 0f;
    private float currentTimer;
    private Player parent;
    private float ratio = 0f;
    [SerializeField] private Image fill;
    [SerializeField] private Gradient gradient;

    private void Update()
    {
        if (maxTimer == 0f)
        {
            return;
        }
        currentTimer = parent.GetCoolDown();
        ratio = currentTimer / maxTimer;
        fill.fillAmount = ratio;
        fill.color = gradient.Evaluate(ratio);
        transform.position = Input.mousePosition;
        if (currentTimer <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void SetMaxTimer(float value)
    {
        maxTimer = value;
    }

    public void SetParent(Player parent)
    {
        this.parent = parent;
    }
}