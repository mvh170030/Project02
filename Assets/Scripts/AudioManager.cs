using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioClip _startingSong;
    public static AudioManager Instance = null;
    AudioSource _audioSource;

    void Start()
    {
        // play starting song on menu start
        if (_startingSong != null)
        {
            AudioManager.Instance.PlaySong(_startingSong);
        }
    }

    private void Awake()
    {
        #region Singleton Pattern (Simple)
        if (Instance == null)
        {
            // doesnt exist yet
            Instance = this;
            DontDestroyOnLoad(gameObject);
            // fill references
            _audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
        #endregion
    }

    public void PlaySong(AudioClip clip)
    {
        _audioSource.clip = clip;
        _audioSource.Play();
    }
}
