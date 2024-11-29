using TMPro;
using UnityEngine;

public class GoldFeedBack : MonoBehaviour
{
    [SerializeField] private float _lifeSpan;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private TextMeshProUGUI _goldText;
    private float _timer = 0.0f;

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > _lifeSpan)
        {
            Destroy(gameObject);
        }
        _rectTransform.position += Vector3.up;
    }

    public void SetParentPosition(Vector3 parentPosition, int valueToDisplay)
    {
        _rectTransform.position = parentPosition;
        _goldText.text += valueToDisplay + " gold";
    }
}
