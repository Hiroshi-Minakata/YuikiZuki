using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Fuyu : MonoBehaviour
{
    [SerializeField] private Image iceObj;
    [SerializeField] private GameObject prevObj;
    [SerializeField] private GameObject nextObj;
    [SerializeField] private GameBase gameBase;
    private Tweener _alphaTweener;
    public bool topSwipe { get; set; }
    public bool bottomSwipe { get; set; }

    void Start()
    {
        gameBase.PrevObj = prevObj;
        gameBase.NextObj = nextObj;
        gameBase.Title.text = "氷を溶かせ！\r\n上下にスワイプしよう！";
        StartCoroutine(gameBase.GameReady());
    }

    void Update()
    {
        if (gameBase.finished)
        {
            gameBase.Fade.gameObject.SetActive(false);
            gameBase.Title.text = "何か出てきましたね！\n開発者に見せてみて！";
        }

        if (!gameBase.IsStarted)
            return;

        // ゲーム開始時に一度だけTweenを開始
        if (_alphaTweener == null)
        {
            _alphaTweener = iceObj.DOFade(0f, gameBase.TimeLimit).SetEase(Ease.InExpo).SetAutoKill(true);
        }

        if (!topSwipe || !bottomSwipe)
            return;

        ++gameBase.Score;
        topSwipe = false;
        bottomSwipe = false;
    }
}
