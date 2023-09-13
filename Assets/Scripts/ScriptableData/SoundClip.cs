using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "SoundEffect", menuName = "Walkros/Sounds")]
public class SoundClip : ScriptableObject
{
    public AudioClip audioClip;
    public AudioMixerGroup MixerGroup;

    [Range(0, 1)] public float Volume = 1;
    [Space]
    [Range(-3, 3)] public float Pitch = 1;

    public void PlayClip(GameObject Source)
    {
        AudioSource audioSource;

        if (Source.TryGetComponent<AudioSource>(out audioSource))
        {

            audioSource.clip = audioClip;
            audioSource.volume = Volume;
            audioSource.pitch = Pitch;

        }
        else
        {
            Source.AddComponent<AudioSource>();
            audioSource = Source.GetComponent<AudioSource>();
        }

        audioSource.outputAudioMixerGroup = MixerGroup;
        audioSource.PlayOneShot(audioClip);
    }
}
