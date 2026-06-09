using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    private Rigidbody2D _rigidbody;

    [SerializeField] private float _moveForce;
    [SerializeField] private float _rotationForce;
    [SerializeField] private float _soundInterval;

    private float _horizontalInput;
    private float _vertivalInput;
    
    private float _nextSoundTimer = 0f;

    public System.Action onMove;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _vertivalInput = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        MoveForward(_vertivalInput);
        Rotate(_horizontalInput);
    }

    private void MoveForward(float vertical)
    {
        _rigidbody.AddForce(transform.up * vertical * _moveForce, ForceMode2D.Impulse);

        if (vertical != 0 && Time.time >= _nextSoundTimer)
        {
            _nextSoundTimer = Time.time + _soundInterval;
            onMove?.Invoke();
        }
    }

    private void Rotate(float horizontal)
    {
        _rigidbody.AddTorque(-horizontal * _rotationForce, ForceMode2D.Impulse);

        if (horizontal != 0 && Time.time >= _nextSoundTimer)
        {
            _nextSoundTimer = Time.time + _soundInterval;
            onMove?.Invoke();
        }
    }
}