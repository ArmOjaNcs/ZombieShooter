using UnityEngine;

public class BloodEffectSpawner : MonoBehaviour
{
    [SerializeField] private BloodEffect _prefab;
    [SerializeField] private int _capacity;

    private ObjectPool<BloodEffect> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<BloodEffect>(_prefab, _capacity, transform);
    }

    public BloodEffect GetBloodEffect()
    {
        BloodEffect effect = _pool.GetElement();
        Initialize(effect);
        return effect;
    }

    private void Initialize(BloodEffect effect)
    {
        effect.PlaybackIsFinished += OnPlaybackIsFinished;
        effect.gameObject.SetActive(true);
    }

    private void OnPlaybackIsFinished(BloodEffect effect)
    {
        effect.gameObject.SetActive(false);
        effect.PlaybackIsFinished -= OnPlaybackIsFinished;
    }
}