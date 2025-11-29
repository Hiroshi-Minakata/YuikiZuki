using DG.Tweening;
using UnityEngine;
using TMPro;

public class BoundText : MonoBehaviour
{
    [SerializeField] float boundPower = 0.25f;
    [SerializeField] float duration = 0.25f;

    private TMP_Text text;
    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
        text = GetComponent<TextMeshProUGUI>();
        text.OnPreRenderText += OnTextChanged;
    }

    void OnDestroy()
    {
        if (text != null)
        {
            text.OnPreRenderText -= OnTextChanged;
        }
    }

    void OnTextChanged(TMP_TextInfo obj)
    {
        transform.DOKill();
        transform.localScale = originalScale * (1f + boundPower);
        transform.DOScale(originalScale, duration).SetEase(Ease.OutBounce);
    }
}