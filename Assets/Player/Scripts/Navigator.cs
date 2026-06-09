using UnityEngine;

public class Navigator : MonoBehaviour
{
    private void Update()
    {
        if (MKSTrigger.Instance != null)
        {
            Vector3 direction = MKSTrigger.Instance.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }
}
