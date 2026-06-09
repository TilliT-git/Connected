using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MKSTrigger : MonoBehaviour
{
    public static MKSTrigger Instance { get; private set; }

    Coroutine _coroutineRestart;

    public System.Action onWin;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    IEnumerator RestartWin()
    {
        yield return new WaitForSeconds(5f);
        GameManager.Instance.Restart();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Docking"))
        {
            PlayerController.Instance.gameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D playerRigitbody);
            PlayerController.Instance.transform.position = Vector3.zero;
            playerRigitbody.velocity = Vector3.zero;
            _coroutineRestart = StartCoroutine(RestartWin());
            onWin?.Invoke();
        }
    }
}
