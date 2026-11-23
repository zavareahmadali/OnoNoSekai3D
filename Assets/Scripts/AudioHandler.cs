using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    [SerializeField] private Dictionary<OnoType, AudioClip> onoAudios;

    private AudioSource audioSource;
    
    private void Start()
    {
        if (audioSource == null)
        {
            audioSource = transform.AddComponent<AudioSource>();
        }
    }

    internal void PlayAudio(OnoType onoType)
    {
        if (audioSource == null || !onoAudios.ContainsKey(onoType))
        {
            Debug.Log("No Audio source or Clip for " + onoType);
            return;
        }
        audioSource.clip = onoAudios[onoType];
        audioSource.Play();
    }
    

}
