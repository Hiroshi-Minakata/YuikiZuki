using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameBase : MonoBehaviour
{
    public float TimeLimit = 30f;
    public float Timer;

    public GameObject PrevObj;
    public GameObject NextObj;
    public TMP_Text Title;
    public TMP_Text Count;
    public Image Fade;
    public Slider TimeBar;
    public bool IsStarted = false;
    private int _score;
    public bool finished = false;
    public int Score
    {
        get => _score;
        set
        {
            _score = value;
            Count.text = _score.ToString();
        }
    }

    public void FixedUpdate()
    {
        if (IsStarted)
        {
            Timer += Time.deltaTime;
            TimeBar.value = Timer / TimeLimit;
            if (Timer >= TimeLimit)
            {
                IsStarted = false;
                StartCoroutine(GameFinish());
            }
        }

    }

    public IEnumerator GameReady()
    {
        finished = false;
        Timer = 0;
        TimeBar.value = 0;
        Fade.gameObject.SetActive(true);
        yield return Fade.DOFade(0f, 2f).WaitForCompletion();
        if (PrevObj != null)
        {
            PrevObj.SetActive(false);
        }
        Fade.gameObject.SetActive(false);
        Count.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        Count.text = "3";
        yield return new WaitForSeconds(1f);
        Count.text = "2";
        yield return new WaitForSeconds(1f);
        Count.text = "1";
        yield return new WaitForSeconds(1f);
        Count.text = "スタート！";
        yield return new WaitForSeconds(1f);
        Count.text = "0";
        IsStarted = true;
    }

    public IEnumerator GameFinish()
    {
        Count.gameObject.SetActive(true);
        Count.text = $"そこまで！\n{Score}";
        yield return new WaitForSeconds(3f);
        Count.text = "";
        yield return Fade.DOFade(1f, 2f).WaitForCompletion();
        Fade.gameObject.SetActive(false);
        if (NextObj != null)
        {
            NextObj.SetActive(true);
        }
        finished = true;
    }
}
