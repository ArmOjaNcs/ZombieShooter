using UnityEngine;

public class AudioPlayerSpawner : MonoBehaviour
{
    [SerializeField] private AudioPlayer _audioPlayerPrefab;
    [SerializeField] private AudioClip _clip;
    [SerializeField] private int _capacity;

    private ObjectPool<AudioPlayer> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<AudioPlayer>(_audioPlayerPrefab, _capacity, transform);
    }

    public AudioPlayer GetAudioPlayer()
    {
        AudioPlayer audioPlayer = _pool.GetElement();
        Initialize(audioPlayer);
        return audioPlayer;
    }

    public void SetAudioClip(AudioClip audioClip)
    {
        _clip = audioClip;
    }

    private void Initialize(AudioPlayer audioPlayer)
    {
        audioPlayer.PlaybackIsFinished += OnPlaybackIsFinished;
        audioPlayer.AudioSource.clip = _clip;
        audioPlayer.transform.position = transform.position;
        audioPlayer.gameObject.SetActive(true);
    }

    private void OnPlaybackIsFinished(AudioPlayer audioPlayer)
    {
        audioPlayer.gameObject.SetActive(false);
        audioPlayer.PlaybackIsFinished -= OnPlaybackIsFinished;
    }
}