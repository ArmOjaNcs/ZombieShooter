using System.Collections.Generic;
using UnityEngine;

public class MuzzleEffect : MonoBehaviour
{
    [SerializeField] private List<ParticleSystem> _effects;
    [SerializeField] private Light _light;

    public void Play()
    {
        foreach (ParticleSystem effect in _effects)
            effect.Play();

        _light.enabled = true;
    }

    public void Stop()
    {
        if (_effects[0].isPlaying == false)
        {
            foreach (ParticleSystem effect in _effects)
                effect.Stop();

            _light.enabled = false;
        }
    }
}