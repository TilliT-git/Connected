using UnityEngine;

public class ObstaclesSpawner : MonoBehaviour
{
    [SerializeField] private float _rangeX;
    [SerializeField] private float _rangeY;
    [SerializeField] private float _minRangeX;
    [SerializeField] private float _minRangeY;
    [SerializeField] private int _obstaclesCount;
    [SerializeField] private float _spawnRadius;
    [SerializeField] private GameObject[] _obstaclesPrefabs;

    private int _obstaclePrefabIndex;

    private void Start()
    {
        ObstacleSpawn();
    }

    private int ObstacleRandom()
    {
        return _obstaclePrefabIndex = Random.Range(0, _obstaclesPrefabs.Length);
    }

    private Vector2 RandomPosition(float rangeX, float rangeY)
    {
        float randomPosX = Random.Range(-_minRangeX - rangeX, _minRangeX + rangeX);
        float randomPosY = Random.Range(-_minRangeY - rangeY, _minRangeY + rangeY);
        return new Vector2(randomPosX, randomPosY);
    }

    private void ObstacleSpawn()
    {
        for (int i = 0; i < _obstaclesCount; i++)
        {
            Vector2 spawnPos = Vector2.zero;
            bool isPositionValid = false;

            int attempts = 0;
            while (!isPositionValid && attempts < 50)
            {
                spawnPos = RandomPosition(_rangeX, _rangeY);
                attempts++;

                Collider2D hitCollider = Physics2D.OverlapCircle(spawnPos, _spawnRadius);

                if (hitCollider == null)
                {
                    isPositionValid = true;
                }
            }

            if (isPositionValid)
            {
                Instantiate(_obstaclesPrefabs[ObstacleRandom()], spawnPos, Quaternion.identity, transform);
            }
        }
    }
}
