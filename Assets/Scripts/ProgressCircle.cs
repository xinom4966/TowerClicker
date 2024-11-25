using UnityEngine;
using UnityEngine.UI;

public class ProgressCircle : MonoBehaviour
{
    private float _maxTimer = 0f;
    private float _currentTimer;
    private Slider _progressSlider;
    private Player _parent;
    [SerializeField] private Image _fill;
    [SerializeField] private Gradient _gradient;

    private void Start()
    {
        _progressSlider = GetComponent<Slider>();
    }

    private void Update()
    {
        if (_maxTimer == 0f)
        {
            return;
        }
        _currentTimer = _parent.GetCoolDown();
        float ratio = _currentTimer / _maxTimer;
        _progressSlider.value = 1 - ratio;
        _fill.color = _gradient.Evaluate(ratio);
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