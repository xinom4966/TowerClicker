using UnityEngine;
using UnityEngine.UI;

public class Ability : MonoBehaviour
{
    [SerializeField] private float cooldown;
    [SerializeField] private Image image;
    protected bool usable = true;
    private float timer = 0.0f;

    protected virtual void Update()
    {
        if (!usable)
        {
            timer += Time.deltaTime;
            CoolDownFeedBack(timer/cooldown);
            if (timer >= cooldown)
            {
                usable = true;
                timer = 0.0f;
            }
        }
    }

    public virtual void Execute()
    {
        
    }

    protected void CoolDownFeedBack(float ratio)
    {
        image.fillAmount = ratio;
    }

    protected void HoverFeedBack()
    {

    }
}
