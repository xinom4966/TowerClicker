using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressGauge : MonoBehaviour
{
    [SerializeField] private Image fill;
    [SerializeField] private Gradient gradient;
    [SerializeField] private TextMeshProUGUI speedUpWarning;

    public void SetFillAmmount(float ratio)
    {
        fill.fillAmount = ratio;
        fill.color = gradient.Evaluate(ratio);
        if (fill.fillAmount == 1)
        {
            speedUpWarning.gameObject.SetActive(true);
            StartCoroutine(WarningFeedBack());
        }
    }

    IEnumerator WarningFeedBack()
    {
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.5f);
            speedUpWarning.gameObject.SetActive(!speedUpWarning.gameObject.activeSelf);
        }
    }
}
