using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    [SerializeField] Image fade;
    [SerializeField] Transform startText;

    void Start()
    {
        Vector3 maxScale = startText.localScale * 1.25f;
        startText.DOScale(maxScale, 1).SetLoops(-1, LoopType.Yoyo);
    }

    public void GameStart()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        yield return fade.DOFade(1f, 2f).WaitForCompletion();
        UnityEngine.SceneManagement.SceneManager.LoadScene("InGame");
    }
}
