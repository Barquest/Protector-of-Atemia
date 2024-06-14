using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MadGeekStudio.ProtectorOfAtemia.Core
{
    public class DialogueVoiceSystem : MonoBehaviour
    {
        [SerializeField] private AudioSource source;

        public void PlayVoice(AudioClip audioClip)
        {
            StopVoice();
            source.clip = audioClip;
            source.Play();
        }
        public void StopVoice()
        {
            if (source.isPlaying)
            {
                source.Stop();
            }
        }
    }
}
