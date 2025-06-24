using UnityEngine;

public class FloatAndSpinInStorm : MonoBehaviour
{
    public Transform stormCenter;      // Tâm của cơn bão
    public float rotationSpeed = 50f;  // Tốc độ xoay vòng (độ/giây)
    public float floatSpeed = 2f;      // Tốc độ bay lên
    public float heightOffset = 5f;    // Độ cao muốn đạt tới
    public float radius = 3f;          // Bán kính bay quanh tâm bão
    public float smoothTime = 0.3f;    // Độ mượt của di chuyển

    public Vector3 minScale = Vector3.zero;  // Kích thước nhỏ nhất
    public float shrinkSpeed = 1f;           // Tốc độ thu nhỏ

    private Vector3 velocity = Vector3.zero;
    private float angle;
    private Vector3 initialScale;

    void Start()
    {
        if (stormCenter == null)
        {
            Debug.LogWarning("Chưa gán stormCenter!");
            enabled = false;
        }

        angle = Random.Range(0f, 360f);
        initialScale = transform.localScale;
    }

    void Update()
    {
        // Xoay quanh tâm bão
        angle += rotationSpeed * Time.deltaTime;
        float rad = angle * Mathf.Deg2Rad;

        Vector3 targetPos = stormCenter.position + new Vector3(
            Mathf.Cos(rad) * radius,
            heightOffset,
            Mathf.Sin(rad) * radius
        );

        // Bay lên một cách mượt mà
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);

        // Thu nhỏ dần
        transform.localScale = Vector3.Lerp(transform.localScale, minScale, shrinkSpeed * Time.deltaTime);

        // Quay quanh trục Y
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
