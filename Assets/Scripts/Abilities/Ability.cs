using UnityEngine;
using UnityEngine.UI;

public class Ability : MonoBehaviour
{
    [SerializeField] private float _cooldown;
    [SerializeField] private Image _image;
    protected bool _usable = true;
    private float _timer = 0.0f;

    protected virtual void Update()
    {
        if (!_usable)
        {
            _timer += Time.deltaTime;
            CoolDownFeedBack(_timer/_cooldown);
            if (_timer >= _cooldown)
            {
                _usable = true;
                _timer = 0.0f;
            }
        }
    }

    public virtual void Execute()
    {
        
    }

    protected void CoolDownFeedBack(float ratio)
    {
        _image.fillAmount = ratio;
    }

    protected void HoverFeedBack()
    {

    }
}
