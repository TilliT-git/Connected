using UnityEngine;
using UnityEngine.UI;

public class BackgroundAnimatedUI : MonoBehaviour
{
    private Image _backgroundImage;
    private Material _material;

    private void Start()
    {
        _backgroundImage = GetComponent<Image>();

        if (_backgroundImage != null)
        {
            _material = _backgroundImage.material;
        }
    }

    private void Update()
    {
        Vector2 offset = _material.mainTextureOffset;

        offset.x += 0.02f * Time.deltaTime;
        offset.y += 0.02f * Time.deltaTime;

        _material.mainTextureOffset = offset;
    }
}
