using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Aki : MonoBehaviour
{
    [SerializeField] private GameObject PrevObj;
    [SerializeField] private GameObject NextObj;
    [SerializeField] private GameBase gameBase;

    [SerializeField] private ParticleSystem donguri;
    [SerializeField] private float clickRadius = 0.5f;

    private ParticleSystem.Particle[] particles;

    void Start()
    {
        gameBase.PrevObj = PrevObj;
        gameBase.NextObj = NextObj;
        gameBase.Title.text = "Ç«ÇÒÇÆÇËÇèEÇ¶ÅI";
        StartCoroutine(gameBase.GameReady());

        particles = new ParticleSystem.Particle[donguri.main.maxParticles];
    }

    void Update()
    {
        if (!gameBase.IsStarted)
            return;

        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            CheckParticleClick();
        }
        else if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            CheckParticleClick();
        }
    }

    private void CheckParticleClick()
    {
        Vector2 screenPosition;
        
        if (Mouse.current != null)
        {
            screenPosition = Mouse.current.position.ReadValue();
        }
        else if (Touchscreen.current != null)
        {
            screenPosition = Touchscreen.current.primaryTouch.position.ReadValue();
        }
        else
        {
            return;
        }

        Vector3 clickPosition = Camera.main.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, Camera.main.nearClipPlane));
        
        int particleCount = donguri.GetParticles(particles);
        
        for (int i = 0; i < particleCount; i++)
        {
            Vector3 particleWorldPosition = donguri.transform.TransformPoint(particles[i].position);
            float distance = Vector2.Distance(clickPosition, particleWorldPosition);
            
            if (distance < clickRadius)
            {
                ++gameBase.Score;
                particles[i].remainingLifetime = 0;
                donguri.SetParticles(particles, particleCount);
                break;
            }
        }
    }
}
