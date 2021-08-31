using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager _instance;

    public GameObject AudioClipPlayerPrefab;

    private void Awake()
    {
        if(_instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public static void PlayClip(AudioClip clip)
    {
        _instance._PlayClip(clip);
    }

    public static void PlayClip(AudioClip[] clips)
    {
        _instance._PlayClip(clips[Random.Range(0, clips.Length)]);
    } 

    void _PlayClip(AudioClip clip)
    {
        AudioSource src = NewSource();
        src.clip = clip;
        src.Play();
        //Destroy(src, 5f);
    }

    AudioSource NewSource()
    {
        GameObject go = Instantiate(AudioClipPlayerPrefab, Vector3.zero, Quaternion.identity);
        go.transform.parent = this.transform;
        AudioSource src = go.GetComponent<AudioSource>();
        return src;
    }
}
