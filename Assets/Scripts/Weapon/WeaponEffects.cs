using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class WeaponEffects : MonoBehaviour
{
    [SerializeField] private MuzzleEffect _muzzleEffect;
    [SerializeField] private DecalEffectSpawner _decalEffectSpawner;
    [SerializeField] private BloodEffectSpawner _bloodEffectSpawner;
    [SerializeField] private AudioPlayerSpawner _audioPlayerSpawner;

    public void StopMuzzleEffect()
    {
        _muzzleEffect.Stop();
    }

    public AudioPlayer GetShootEffectAudioPlayer()
    {
        return _audioPlayerSpawner.GetAudioPlayer();
    }

    public void PlayMuzzleEffect()
    {
        _muzzleEffect.Play();
    }

    public DecalEffect GetDecalEffect()
    {
        return _decalEffectSpawner.GetDecalEffect();
    }

    public BloodEffect GetBloodEffect()
    {
        return _bloodEffectSpawner.GetBloodEffect();
    }
}