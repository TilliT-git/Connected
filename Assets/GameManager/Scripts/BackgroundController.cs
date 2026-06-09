using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    private Rigidbody2D _playerRb;
    private Material _material;

    private void Start()
    {
        _playerRb = PlayerController.Instance.GetComponent<Rigidbody2D>();
        _material = GetComponent<SpriteRenderer>().material;
    }

    private void Update()
    {
        if (PlayerController.Instance.transform != null)
        {
            transform.position = new Vector3(PlayerController.Instance.transform.position.x, PlayerController.Instance.transform.position.y, transform.position.z);

            Vector2 offset = _material.mainTextureOffset;

            offset.x += _playerRb.velocity.x * 0.01f * Time.deltaTime;
            offset.y += _playerRb.velocity.y * 0.01f * Time.deltaTime;

            _material.mainTextureOffset = offset;
        }
    }
}
