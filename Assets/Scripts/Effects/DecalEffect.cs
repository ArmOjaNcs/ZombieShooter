using System;
using UnityEngine;

public class DecalEffect : Effect
{
    public event Action<DecalEffect> PlaybackIsFinished;

    public ParticleSystem ParticleSystem { get; private set; }
 
    private protected override void Awake()
    {
        base.Awake();
        ParticleSystem = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (ParticleSystem.isPlaying == false && IsFinished == false)
        {
            PlaybackIsFinished?.Invoke(this);
            IsFinished = true;
        }
    }
}