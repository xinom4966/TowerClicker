using TMPro;
using UnityEngine;

public class GoldFeedBack : MonoBehaviour
{
    [SerializeField] private float lifeSpan;
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private TextMeshProUGUI goldText;
    private float timer = 0.0f;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > lifeSpan)
        {
            Destroy(gameObject);
        }
        rectTransform.position += Vector3.up;
    }

    public void SetDatas(Vector3 parentPosition, int valueToDisplay)
    {
        rectTransform.position = parentPosition;
        goldText.text += valueToDisplay + " gold";
    }
}
