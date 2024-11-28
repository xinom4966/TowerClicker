using UnityEngine;

public class GoldFeedBack : MonoBehaviour
{
    [SerializeField] private float _lifeSpan;
    [SerializeField] private RectTransform _rectTransform;
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

    public void SetParentPosition(Vector3 parentPosition)
    {
        _rectTransform.position = parentPosition;
    }
}
