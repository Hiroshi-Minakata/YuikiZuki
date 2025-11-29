using DG.Tweening;
using UnityEngine;

public class EasingObject : MonoBehaviour
{
    [SerializeField] private Vector3 toPos;
    [SerializeField] private Quaternion toRot;
    [SerializeField] private Vector3 toScl = Vector3.one;
    [SerializeField] private float durationPos;
    [SerializeField] private float durationRot;
    [SerializeField] private float durationScl;

    [SerializeField] private Ease easeType;
    [SerializeField] private int loops = -1;

    private Tween _posTween;
    private Tween _rotTween;
    private Tween _sclTween;

    void Start()
    {
        CreatePositionTween();
        CreateRotationTween();
        CreateScaleTween();
    }

    private void CreatePositionTween()
    {
        if (durationPos == 0)
            return;

        _posTween = transform.DOLocalMove(toPos, durationPos).SetEase(easeType);
        _posTween.SetLoops(loops, LoopType.Yoyo);
    }

    private void CreateRotationTween()
    {
        if (durationRot == 0)
            return;

        _rotTween = transform.DOLocalRotateQuaternion(toRot, durationRot).SetEase(easeType);
        _rotTween.SetLoops(loops, LoopType.Yoyo);
    }

    private void CreateScaleTween()
    {
        if (durationScl == 0)
            return;

        _sclTween = transform.DOScale(toScl, durationScl).SetEase(easeType);
        _sclTween.SetLoops(loops, LoopType.Yoyo);
    }

    void OnDisable()
    {
        _posTween?.Kill();
        _rotTween?.Kill();
        _sclTween?.Kill();
    }

    void OnDestroy()
    {
        _posTween?.Kill();
        _rotTween?.Kill();
        _sclTween?.Kill();
    }
}