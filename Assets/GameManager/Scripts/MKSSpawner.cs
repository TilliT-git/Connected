using UnityEngine;

public class MKSSpawner : MonoBehaviour
{
    [SerializeField] private float _rangeX;
    [SerializeField] private float _rangeY;
    [SerializeField] private float _minRangeX;
    [SerializeField] private float _minRangeY;

    [SerializeField] private GameObject _MKSPrefab;

    private void Start()
    {
        Spawn();
    }

    private Vector2 RandomPosition(float rangeX, float rangeY)
    {
        float randomPosX = Random.Range(-_minRangeX - rangeX, _minRangeX + rangeX);
        float randomPosY = Random.Range(-_minRangeY - rangeY, _minRangeY + rangeY);
        return new Vector2( randomPosX, randomPosY);
    }

    private void Spawn()
    {
        Vector3 spawnPos = RandomPosition(_rangeX, _rangeY);
        Vector3 direction = transform.position - spawnPos;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _MKSPrefab.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        GameObject mks = Instantiate(_MKSPrefab, spawnPos, _MKSPrefab.transform.rotation);
        MKSTrigger mksTrigger = mks.GetComponentInChildren<MKSTrigger>();

        if (mksTrigger != null)
        {
            mksTrigger.onWin += GameManager.Instance.Win;
        }
    }
}
