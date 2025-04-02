using System;
using System.Collections.Generic;
using UnityEngine;

public class BloodEffect : Effect
{
    [SerializeField] private List<ParticleSystem> _particles;

    public event Action<BloodEffect> PlaybackIsFinished;

    private void Update()
    {
        if (_particles[1].isPlaying == false && IsFinished == false)
        {
            PlaybackIsFinished?.Invoke(this);
            IsFinished = true;
        }
    }
}