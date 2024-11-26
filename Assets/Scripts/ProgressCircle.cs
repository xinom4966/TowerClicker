using UnityEngine;
using UnityEngine.UI;

public class ProgressCircle : MonoBehaviour
{
    private float _maxTimer = 0f;
    private float _currentTimer;
    private Player _parent;
    [SerializeField] private Image _fill;
    [SerializeField] private Gradient _gradient;

    private void Update()
    {
        if (_maxTimer == 0f)
        {
            return;
        }
        _currentTimer = _parent.GetCoolDown();
        float ratio = _currentTimer / _maxTimer;
        _fill.fillAmount = ratio;
        _fill.color = _gradient.Evaluate(ratio);
        transform.position = Input.mousePosition;
        if (_currentTimer <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void SetMaxTimer(float value)
    {
        _maxTimer = value;
    }

    public void SetParent(Player parent)
    {
        _parent = parent;
    }
}