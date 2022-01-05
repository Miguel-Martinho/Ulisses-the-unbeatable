using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    AudioSource source;

    [SerializeField]
    AudioClip clip;
    private static MusicManager _instance;

    public static MusicManager Instance { get { return _instance; } }

    private void Awake()
    {
        Time.timeScale = 1;
        DontDestroyOnLoad(gameObject);
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        source = GetComponent<AudioSource>();
        source.clip = clip;
        source.Play();
    }


}
