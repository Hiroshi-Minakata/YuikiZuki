using System.Runtime.InteropServices;
using UnityEngine;

public class MicrophoneInput : MonoBehaviour
{
#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern int InitMicrophone();

    [DllImport("__Internal")]
    private static extern float GetMicrophoneVolume();

    [DllImport("__Internal")]
    private static extern void StopMicrophone();
#endif

    private AudioClip microphoneClip;
    private float[] samples = new float[128];
    private bool isWebGL = false;

    void Start()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        isWebGL = true;
        InitMicrophone();
#else
        if (Microphone.devices.Length > 0)
        {
            microphoneClip = Microphone.Start(null, true, 1, 44100);
        }
#endif
    }

    public float GetVolume()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        return GetMicrophoneVolume();
#else
        if (microphoneClip == null || !Microphone.IsRecording(null))
        {
            return 0f;
        }

        int micPosition = Microphone.GetPosition(null) - (samples.Length + 1);
        if (micPosition < 0)
        {
            return 0f;
        }

        microphoneClip.GetData(samples, micPosition);

        float sum = 0f;
        for (int i = 0; i < samples.Length; i++)
        {
            sum += Mathf.Abs(samples[i]);
        }

        return sum / samples.Length;
#endif
    }

    void OnDestroy()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        StopMicrophone();
#else
        if (Microphone.IsRecording(null))
        {
            Microphone.End(null);
        }
#endif
    }
}
