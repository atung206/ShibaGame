using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundtrack : MonoBehaviour
{
    private AudioSource source;
    public AudioClip[] audioClips;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        StartCoroutine(PlayAudio());
    }

    IEnumerator PlayAudio()
    {
        foreach (AudioClip clip in audioClips)
        {
            source.clip = clip;

            source.loop = clip.name.ToLower().Contains("loop");

            source.Play();
            while (source.isPlaying)
            {
                yield return null;
            }
        }
    }
}
