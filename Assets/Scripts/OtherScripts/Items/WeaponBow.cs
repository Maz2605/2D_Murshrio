using UnityEngine;

public class WeaponBow : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float arrowSpeed = 10f;
    [SerializeField] private Transform playerTransform;
    private CharacterController characterController;

    private SpriteRenderer bowRenderer;
    private SpriteRenderer playerSpriteRenderer;

    private void Awake()
    {
        bowRenderer = GetComponent<SpriteRenderer>();
        playerSpriteRenderer = playerTransform.GetComponent<SpriteRenderer>();
        characterController = playerTransform.GetComponent<CharacterController>();
    }

    private void Update()
    {
/*        if(!characterController.CanControl)return;
*/        RotateToMouse();

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void RotateToMouse()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mouseWorldPos - transform.position;
        direction.z = 0;

        bool isFacingRight = !playerSpriteRenderer.flipX;
        Vector2 playerFacingDir = isFacingRight ? Vector2.right : Vector2.left;
        float angle = Vector2.Angle(playerFacingDir, direction.normalized);

        if (angle < 90f)
        {
            float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
            bowRenderer.flipY = playerTransform.localScale.x < 0;
        }
    }

    private void Shoot()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 shootDir = (mouseWorldPos - firePoint.position).normalized;

        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        rb.velocity = shootDir * arrowSpeed;

        float angle = Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg;
        arrow.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
