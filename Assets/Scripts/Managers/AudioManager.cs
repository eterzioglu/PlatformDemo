using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip perfectTiming;
    [SerializeField] private AudioSource audioSource;

    #region Singleton
    public static AudioManager instance = null;
    private void Awake()
    {
        if (instance == null) instance = this;
    }
    #endregion

    public void Success(float pitch)
    {
        pitch = Mathf.Clamp(pitch, 0.5f, 3);
        audioSource.pitch = pitch;
        audioSource.Pause();
        audioSource.PlayOneShot(perfectTiming);
    }
}
