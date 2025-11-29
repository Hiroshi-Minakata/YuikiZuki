using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Haru : MonoBehaviour
{
    [SerializeField] private GameObject PrevObj;
    [SerializeField] private GameObject NextObj;
    [SerializeField] private GameBase gameBase;
    [SerializeField] private ParticleSystem watage;

    void Start()
    {
        gameBase.PrevObj = PrevObj;
        gameBase.NextObj = NextObj;
        StartCoroutine(StartSequence());
    }

    void Update()
    {
        if (!gameBase.IsStarted)
            return;

        ++gameBase.Score;
        watage.Play();
    }

    private IEnumerator StartSequence()
    {
        yield return StartCoroutine(Description());
        yield return StartCoroutine(gameBase.GameReady());
    }

    private IEnumerator Description()
    {
        gameBase.gameObject.SetActive(true);
        yield return gameBase.Fade.DOFade(0f, 2f).WaitForCompletion();
        var title = gameBase.Title;
        yield return new WaitForSeconds(3f);
        title.text = "はじめまして！";
        yield return new WaitForSeconds(3f);
        title.text = "今から始まるのは\n4つのミニゲームです！";
        yield return new WaitForSeconds(3f);
        title.text = "最後までクリアすると\nいい事があるかも...！";
        yield return new WaitForSeconds(3f);
        title.text = "準備はOK？";
        yield return new WaitForSeconds(3f);
        title.text = "たんぽぽを飛ばせ！\r\nマイクに息を吹こう！";
    }
}
