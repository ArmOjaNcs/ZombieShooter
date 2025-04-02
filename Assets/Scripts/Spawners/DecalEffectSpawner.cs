using UnityEngine;

public class DecalEffectSpawner : MonoBehaviour
{
    [SerializeField] private DecalEffect _prefab;
    [SerializeField] private int _capacity;

    private ObjectPool<DecalEffect> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<DecalEffect>(_prefab, _capacity, transform);
    }

    public DecalEffect GetDecalEffect()
    {
        DecalEffect effect = _pool.GetElement();
        Initialize(effect);
        return effect;
    }

    private void Initialize(DecalEffect effect)
    {
        effect.PlaybackIsFinished += OnPlaybackIsFinished;
        effect.gameObject.SetActive(true);
    }

    private void OnPlaybackIsFinished(DecalEffect effect)
    {
        effect.gameObject.SetActive(false);
        effect.PlaybackIsFinished -= OnPlaybackIsFinished;
    }
}