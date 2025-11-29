using UnityEngine;

public class Aki : MonoBehaviour
{
    [SerializeField] private GameObject PrevObj;
    [SerializeField] private GameObject NextObj;
    [SerializeField] private GameBase gameBase;

    [SerializeField] private ParticleSystem donguri;

    void Start()
    {
        gameBase.PrevObj = PrevObj;
        gameBase.NextObj = NextObj;
        gameBase.Title.text = "どんぐりを落とせ！\r\nスマホを振ろう！";
        StartCoroutine(gameBase.GameReady());
    }

    void Update()
    {
        if (!gameBase.IsStarted)
            return;
    }
}
