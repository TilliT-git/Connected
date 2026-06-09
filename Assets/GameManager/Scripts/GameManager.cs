using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject _startScreen;
    //[SerializeField] private GameObject _menuScreen;
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private GameObject _loseScreen;

    //private bool _isPause = false;

    public System.Action onClick;
    public System.Action onDeath;
    public System.Action onConnected;

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
        PlayerController.Instance.enabled = false;
        _startScreen.SetActive(true);
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape) && !_startScreen.activeSelf) Pause();
    }

    public void StartGame()
    {
        PlayerController.Instance.enabled = true;
        _startScreen.SetActive(false);
        onClick?.Invoke();
    }

    //private void Pause()
    //{
    //    _isPause = !_isPause;

    //    if (_isPause)
    //    {
    //        Time.timeScale = 0f;
    //        PlayerController.Instance.enabled = false;
    //        _menuScreen.SetActive(true);
    //        onClick?.Invoke();
    //    }
    //    else
    //    {
    //        Time.timeScale = 1f;
    //        PlayerController.Instance.enabled = true;
    //        _menuScreen.SetActive(false);
    //    }

    //    onClick?.Invoke();
    //}

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Win()
    {
        WinScreenView();
    }

    public void GameOver()
    {
        LoseScreenView();
    }

    private void WinScreenView()
    {
        PlayerController.Instance.enabled = false;
        _winScreen.SetActive(true);
        onConnected?.Invoke();
    }

    private void LoseScreenView()
    {
        PlayerController.Instance.enabled = false;
        _loseScreen.SetActive(true);
        onDeath?.Invoke();
    }

    private void OnDisable()
    {
        if (MKSTrigger.Instance != null)
        {
            MKSTrigger.Instance.onWin -= Win;
        }
    }
}