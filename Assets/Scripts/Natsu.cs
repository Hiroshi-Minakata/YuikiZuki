using UnityEngine;

public class Natsu : MonoBehaviour
{
    [SerializeField] private GameObject PrevObj;
    [SerializeField] private GameObject NextObj;
    [SerializeField] private GameBase gameBase;
    [SerializeField] private ParticleSystem chip;

    [SerializeField] private GameObject suika;
    [SerializeField] private GameObject breakedSuika;
    [SerializeField] private RectTransform stick;

    void Start()
    {
        gameBase.PrevObj = PrevObj;
        gameBase.NextObj = NextObj;
        gameBase.Title.text = "スイカを割れ！\r\n連打しよう！";
        StartCoroutine(gameBase.GameReady());
    }

    void Update()
    {
        if (!gameBase.IsStarted)
            return;
    }

    public void OnClick()
    {
        ++gameBase.Score;
        chip.Play();
        if (suika.activeSelf)
        {
            suika.SetActive(false);
            breakedSuika.SetActive(true);
            stick.rotation = Quaternion.Euler(0, 0, -14);
        }
        else
        {
            suika.SetActive(true);
            breakedSuika.SetActive(false);
            stick.rotation = Quaternion.Euler(0, 0, 40);
        }
    }
}
