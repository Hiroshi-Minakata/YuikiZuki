using DG.Tweening;
using UnityEngine;

public class Title : MonoBehaviour
{
    [SerializeField] Transform startText;

    void Start()
    {
        Vector3 maxScale = startText.localScale * 1.25f;
        startText.DOScale(maxScale, 1).SetLoops(-1, LoopType.Yoyo);
    }
}
