using UnityEngine;
using UnityEngine.Audio;
using System.Linq;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioBank audioBank;

    private static AudioSource audioSource;
    public static AudioManager _instance;

    private void Awake()
    {
        audioSource = this.GetComponent<AudioSource>();

        if (_instance == null)
            _instance = this;
        else
            Destroy(this);
    }

    public void PlayNotificationSound()
    {
        foreach(AudioClip audioClip in audioBank.audioClips)
        {
            if(audioClip.name == "Notification")
            {
                audioSource.PlayOneShot(audioClip);
                return;
            }
        }
    }

    public void PlayClickNegativeSound()
    {
        foreach (AudioClip audioClip in audioBank.audioClips)
        {
            if (audioClip.name == "ClickNegative")
            {
                audioSource.PlayOneShot(audioClip);
                return;
            }
        }
    }

    public void PlayClickNeutralSound()
    {
        foreach (AudioClip audioClip in audioBank.audioClips)
        {
            if (audioClip.name == "ClickNeutral")
            {
                audioSource.PlayOneShot(audioClip);
                return;
            }
        }
    }

    public void PlayPushNeutralSound()
    {
        foreach (AudioClip audioClip in audioBank.audioClips)
        {
            if (audioClip.name == "PushNeutral")
            {
                audioSource.PlayOneShot(audioClip, 0.5f);
                return;
            }
        }
    }

    public void PlayUnlockSound()
    {
        foreach (AudioClip audioClip in audioBank.audioClips)
        {
            if (audioClip.name == "Unlock")
            {
                audioSource.PlayOneShot(audioClip, 0.4f);
                return;
            }
        }
    }
}
