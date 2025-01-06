using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressGauge : MonoBehaviour
{
    [SerializeField] private Image _fill;
    [SerializeField] private Gradient _gradient;
    [SerializeField] private TextMeshProUGUI _speedUpWarning;

    public void SetFillAmmount(float ratio)
    {
        _fill.fillAmount = ratio;
        _fill.color = _gradient.Evaluate(ratio);
        if (_fill.fillAmount == 1)
        {
            _speedUpWarning.gameObject.SetActive(true);
            StartCoroutine(WarningFeedBack());
        }
    }

    IEnumerator WarningFeedBack()
    {
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(1f);
            _speedUpWarning.gameObject.SetActive(!_speedUpWarning.gameObject.activeSelf);
        }
    }
}
