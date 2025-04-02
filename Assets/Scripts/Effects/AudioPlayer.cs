using System;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    private bool _isFinished;

    public event Action<AudioPlayer> PlaybackIsFinished;

    public AudioSource AudioSource { get; private set; }

    private void Awake()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _isFinished = false;
    }

    private void Update()
    {
        if (AudioSource.isPlaying == false && _isFinished == false)
        {
            PlaybackIsFinished?.Invoke(this);
            _isFinished = true;
        }
    }
}