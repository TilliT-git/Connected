using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public GameObject player;

    [Header("Settings")]
    public float maxAttractionForce = 10f;
    public float minAttractionForce = 1f;
    public float attractionRadius = 5f;
    public AnimationCurve forceCurve = AnimationCurve.EaseInOut(0, 1, 1, 0.75f);

    private Rigidbody2D playerRb;
    private Transform playerTransform;

    public System.Action onLose;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = PlayerController.Instance.transform;
        playerRb = player.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 directionToPlayer = playerTransform.position - transform.position;
        float distance = directionToPlayer.magnitude;

        if (distance <= attractionRadius && distance > 0.2f)
        {
            float forceMultiplier = forceCurve.Evaluate(1 - (distance / attractionRadius));
            float currentForce = Mathf.Lerp(minAttractionForce, maxAttractionForce, forceMultiplier);
            float realForce = currentForce / (distance * distance) * 0.5f;
            Vector2 forceDirection = directionToPlayer.normalized;

            playerRb.AddForce(-forceDirection * realForce, ForceMode2D.Impulse);

            Vector2 tangentialVelocity = GetTangentialVelocity(playerRb.velocity, forceDirection);
            float dampingFactor = Mathf.Lerp(0, 0.95f, 1 - (distance / attractionRadius));
            playerRb.velocity -= tangentialVelocity * dampingFactor * Time.fixedDeltaTime;
        }
    }

    private Vector2 GetTangentialVelocity(Vector2 velocity, Vector2 radialDirection)
    {
        Vector2 radialVelocity = Vector2.Dot(velocity, radialDirection) * radialDirection;
        return velocity - radialVelocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.GameOver();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0.5f, 0, 0.5f, 0.3f);
        Gizmos.DrawWireSphere(transform.position, attractionRadius);

        Gizmos.color = new Color(0.5f, 0, 0.5f, 0.1f);
        Gizmos.DrawSphere(transform.position, attractionRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.1f);


        int segments = 12;
        for (int i = 0; i < segments; i++)
        {
            float angle = (i * 360f / segments) * Mathf.Deg2Rad;
            float radius = attractionRadius * 0.8f;
            Vector3 direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
            Vector3 point = transform.position + direction * radius;

            float normalizedDistance = radius / attractionRadius;
            float force = forceCurve.Evaluate(1 - normalizedDistance);

            Gizmos.color = Color.Lerp(Color.blue, Color.red, force);
            Gizmos.DrawRay(point, -direction * force * 0.5f);
        }

    }
}