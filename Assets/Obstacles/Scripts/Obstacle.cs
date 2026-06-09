using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private Transform _playerTransform;

    private Rigidbody2D _obstacleRigidbody;

    [SerializeField] private float _forceRotation;

    public System.Action onLose;

    private void Start()
    {
        _playerTransform = PlayerController.Instance.transform;
        _obstacleRigidbody = GetComponent<Rigidbody2D>();
        Rotate();
    }

    private float GetRandomForceRotation()
    {
        return Random.Range(-_forceRotation, _forceRotation);
    }

    private void Rotate()
    {
        _obstacleRigidbody.AddTorque(GetRandomForceRotation(), ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.GameOver();
        }
    }
}