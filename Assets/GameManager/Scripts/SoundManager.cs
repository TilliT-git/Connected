using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    private PlayerController _playerController;
    
    private AudioSource _audioSource;

    [SerializeField] private AudioClip _moveSound;
    [SerializeField] private AudioClip _clickSound;
    [SerializeField] private AudioClip _deathSound;
    [SerializeField] private AudioClip _connectSound;
    [SerializeField] private AudioClip _winSound;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        _playerController = PlayerController.Instance.GetComponent<PlayerController>();
        _audioSource = GetComponent<AudioSource>();

        if (GameManager.Instance != null)
        {
            GameManager.Instance.onClick += ClickSound;
            GameManager.Instance.onDeath += DeathSound;
            GameManager.Instance.onConnected += ConnectSound;
            GameManager.Instance.onConnected += WinSound;
        }

        _playerController.onMove += MoveSound;
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.onClick -= ClickSound;
            GameManager.Instance.onDeath -= DeathSound;
            GameManager.Instance.onConnected -= ConnectSound;
            GameManager.Instance.onConnected -= WinSound;
        }

        _playerController.onMove -= MoveSound;
    }

    private void MoveSound() => _audioSource.PlayOneShot(_moveSound);
    private void ClickSound() => _audioSource.PlayOneShot(_clickSound);
    private void DeathSound() => _audioSource.PlayOneShot(_deathSound);
    private void ConnectSound() => _audioSource.PlayOneShot(_connectSound);
    private void WinSound() => _audioSource.PlayOneShot(_winSound);
}